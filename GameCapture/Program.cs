using System;
using System.Diagnostics;
using System.Drawing;
using OpenCvSharp;

class Program
{
    static void Main()
    {
        // Geben Sie den Namen der Anwendung an, von der Sie einen Screenshot machen möchten
        string targetApplicationName = "Destiny2"; // Ändern Sie dies entsprechend

        // Finden Sie das Prozessobjekt für die Zielanwendung
        Process targetProcess = FindProcessByName(targetApplicationName);

        if (targetProcess != null)
        {
            IntPtr mainWindowHandle = targetProcess.MainWindowHandle;

            Mat screenshot = CaptureWindow(mainWindowHandle);

            Cv2.ImShow("Screenshot", screenshot);
            Cv2.WaitKey(0);

            Cv2.DestroyAllWindows();
            screenshot.Dispose();
        }
        else
        {
            Console.WriteLine($"Die Anwendung '{targetApplicationName}' wurde nicht gefunden.");
        }
    }

    static Process FindProcessByName(string processName)
    {
        Process[] processes = Process.GetProcessesByName(processName);
        return processes.Length > 0 ? processes[0] : null;
    }

    static Mat CaptureWindow(IntPtr hWnd)
    {
        Rect windowRect = GetWindowRect(hWnd);

        // Erstellen Sie ein Mat-Objekt für die Bildschirmaufnahme
        Mat screenshot = new Mat();

        // Capture the window
        using (var screenCapture = new ScreenCapture())
        {
            screenCapture.CaptureWindow(windowRect);
            screenshot = screenCapture.CapturedImage;
        }

        return screenshot;
    }

    static Rect GetWindowRect(IntPtr hWnd)
    {
        User32.GetWindowRect(hWnd, out var rect);



        return rect;
    }
}

class ScreenCapture : IDisposable
{
    private Mat capturedImage;

    public Mat CapturedImage => capturedImage;

    public void CaptureWindow(Rect rect)
    {
        // Implementieren Sie hier die Logik zur Bildschirmaufnahme
        // Verwenden Sie ggf. die Klasse Mat und OpenCVSharp-Methoden

        // Beispiel:
        capturedImage = new Mat(rect.Height, rect.Width, MatType.CV_8UC3, Scalar.All(0)); // Platzhalter für die eigentliche Logik
    }

    public void Dispose()
    {
        // Fügen Sie hier ggf. Code für die Freigabe von Ressourcen hinzu
    }
}

class User32
{
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);
}