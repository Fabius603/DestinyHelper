using OpenCvSharp;
using OpenCvSharp.Features2D;
using OpenCvSharp.XFeatures2D;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Windows.Navigation;

namespace DestinyHelper
{
    public class BildMatcher
    {
        const int MIN_MATCH_COUNT = 0; // Mindestanzahl der übereinstimmenden KeyPoints
        const double GENAUIGKEIT = 0.8; // Wie genau soll die Übereinstimmung sein

        struct TrainerValues
        {
            public KeyPoint[] KP_TRAIN;
            public Mat DES_TRAIN;
            public Mat IMG;
        }


        public static MatcherResults MatchImages(Mat queryImage, Mat trainImage, Rect ROI)
        {
            MatcherResults matcherResults = new MatcherResults();
            matcherResults.img = queryImage;
            TrainerValues TV = TrainerLaden(trainImage);
            matcherResults = Vergleichen(queryImage, trainImage, TV.KP_TRAIN, TV.DES_TRAIN, matcherResults, ROI);
            trainImage.Dispose();
            return matcherResults;
        }

        static MatcherResults Vergleichen(Mat queryImage, Mat trainImage, KeyPoint[] kp_train, Mat des_train, MatcherResults matcherResults, Rect ROI)
        {
            // keyPoints und destination wird mit sift berechnet
            using (SIFT sift = SIFT.Create())
            {
                KeyPoint[] kp_query;
                using (Mat des_query = new Mat())
                {
                    sift.DetectAndCompute(queryImage, null, out kp_query, des_query);

                    // KeyPoints der Bilder werden verglichen und alle mit einer Abweichung unter GENAUIGKEIT werden in goodMatches gespeichert
                    using (FlannBasedMatcher matcher = new FlannBasedMatcher())
                    {
                        if (des_train.Rows == 0 || des_query.Rows == 0)
                        {
                            matcherResults.Success = false;
                            return matcherResults;
                        }
                        DMatch[][] matches;
                        try
                        {
                            matches = matcher.KnnMatch(des_train, des_query, 2);
                        } catch (Exception e) 
                        {
                            matcherResults.Success = false;
                            return matcherResults;
                        }

                        var goodMatches = matches.Where(m => m[0].Distance < GENAUIGKEIT * m[1].Distance).Select(m => m[0]).ToArray(); //---> wirkt negativ auf Ergebnis ?!?
                        //var goodMatches = matches.Select(m => m[0]).ToArray();

                        if (goodMatches.Length >= MIN_MATCH_COUNT)
                        {
                            // Punkte der guten Matches werden berechnet
                            Point2f[] queryPoints = goodMatches.Select(match => kp_query[match.TrainIdx].Pt).ToArray();
                            Point2f[] trainPoints = goodMatches.Select(match => kp_train[match.QueryIdx].Pt).ToArray();

                            if(queryPoints.Length <= 4 || trainPoints.Length <= 4)
                            {
                                matcherResults.Success = false;
                                return matcherResults;
                            }

                            // Homography wird erstellt -> Matrix mit verschiedenen Umrechnungsfaktoren
                            using (Mat homography = Cv2.FindHomography(InputArray.Create(trainPoints), InputArray.Create(queryPoints), HomographyMethods.Ransac, 5.0))
                            {
                                // Eckpunkte des trainImage werden mit Umrechnungsfaktoren für queryImage berechnet
                                Point2f[] points = { new Point2f(0, 0), new Point2f(0, trainImage.Rows - 1), new Point2f(trainImage.Cols - 1, trainImage.Rows - 1), new Point2f(trainImage.Cols - 1, 0) };
                                Point2f[] destination = null;
                                try
                                {
                                    destination = Cv2.PerspectiveTransform(points, homography);
                                } catch 
                                {
                                    matcherResults.Success = false;
                                    return matcherResults;
                                }


                                IEnumerable<OpenCvSharp.KeyPoint> keyPointEnumerable = ConvertToEnumerable(destination);
                                Cv2.DrawKeypoints(queryImage, keyPointEnumerable, queryImage, Scalar.Lime, DrawMatchesFlags.Default);
                                DrawRectangle(destination, matcherResults.img);

                                // rotations Matrix wird aus homography entnommen
                                double rotationAngleDegrees = CalcRotation(homography);

                                double[,] realPosition = RealPosition(ROI, destination);

                                matcherResults.realPosition = realPosition;
                                matcherResults.destination = destination; 
                                matcherResults.RotationAngleDegrees = rotationAngleDegrees;
                                matcherResults.goodMatches = goodMatches;
                                matcherResults.Success = true;
                                return matcherResults;
                            }
                        }
                        else
                        {
                            matcherResults.Success = false;
                            return matcherResults;
                        }
                    }
                }
            }
        }

        static TrainerValues TrainerLaden(Mat trainImage)
        {
            Cv2.CvtColor(trainImage, trainImage, ColorConversionCodes.BGR2GRAY);
            using (SIFT sift = SIFT.Create())
            {
                TrainerValues TV = new TrainerValues();
                TV.DES_TRAIN = new Mat();
                sift.DetectAndCompute(trainImage, null, out TV.KP_TRAIN, TV.DES_TRAIN);
                return TV;
            }
        }

        static IEnumerable<KeyPoint> ConvertToEnumerable(Point2f[] array)
        {
            foreach (var point2f in array)
            {
                yield return new KeyPoint(point2f, 0);
            }
        }

        static Point[] ConvertToPointArray(Point2f[] array)
        {
            Point[] resultArray = new Point[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                resultArray[i] = new Point((int)array[i].X, (int)array[i].Y);
            }

            return resultArray;
        }

        static double CalcRotation(Mat homography)
        {
            // rotations Matrix wird aus homography entnommen
            double[,] rotationMatrix = new double[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    rotationMatrix[i, j] = homography.At<double>(i, j);
                }
            }
            double rotationAngle = Math.Atan2(rotationMatrix[1, 0], rotationMatrix[0, 0]);
            return rotationAngle * (180.0 / Math.PI);
        }

        static void DrawRectangle(Point2f[] eckpunkte, Mat img)
        {
            Point[] pointArray = ConvertToPointArray(eckpunkte);
            Cv2.Line(img, pointArray[0], pointArray[1], Scalar.AliceBlue, thickness: 1, lineType: LineTypes.Link8);
            Cv2.Line(img, pointArray[1], pointArray[2], Scalar.AliceBlue, thickness: 1, lineType: LineTypes.Link8);
            Cv2.Line(img, pointArray[2], pointArray[3], Scalar.AliceBlue, thickness: 1, lineType: LineTypes.Link8);
            Cv2.Line(img, pointArray[3], pointArray[0], Scalar.AliceBlue, thickness: 1, lineType: LineTypes.Link8);
        }

        static double[,] RealPosition(Rect roi, Point2f[] destination)
        {
            double[] P1 = { (double)roi.X + (double)destination[0].X, (double)roi.Y + (double)destination[0].Y };
            double[] P2 = { (double)roi.X + (double)destination[1].X, (double)roi.Y + (double)destination[1].Y };
            double[] P3 = { (double)roi.X + (double)destination[2].X, (double)roi.Y + (double)destination[2].Y };
            double[] P4 = { (double)roi.X + (double)destination[3].X, (double)roi.Y + (double)destination[3].Y };
            double[,] realPosition = { { P1[0], P1[1] }, { P2[0], P2[1] }, { P3[0], P3[1] }, { P4[0], P4[1] } };
            return realPosition;
        }
    }
}
