using System.Net.Sockets; // Að iletiþimi için gerekli sýnýflarý içerir (TcpClient, UdpClient, vb.)
using System.Net;         // IP adresleri ve að iletiþimi için gerekli sýnýflarý içerir (IPAddress, IPEndPoint, vb.)
using System.Text;        // Metin kodlama ve dönüþtürme iþlemleri için gerekli sýnýflarý içerir (Encoding, vb.)
using System;             // Temel sistem iþlemleri ve hata yakalama için gerekli

namespace UDP_TCP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); // Form bileþenlerini baþlatýr (Windows Forms tasarýmý için otomatik oluþturulur)
        }

        private void btnTCPPing_Click(object sender, EventArgs e)
        {
            try
            {
                // Telefonunuzdaki sunucu bilgileri
                string serverIP = "ip giriniz örnek: 12.168.154.6"; // Sunucunun IP adresi (yerel aðda)
                int port = 7070; // portunuzu giriniz örnek 7070; // Sunucunun dinlediði port numarasý

                // TCP istemci oluþtur ve baðlan
                using TcpClient client = new TcpClient(); // TcpClient nesnesi oluþturulur
                client.ConnectAsync(serverIP, port).Wait(5000); // Sunucuya asenkron olarak baðlanýr ve 5 saniye bekler

                if (client.Connected) // Baðlantý baþarýlýysa
                {
                    string message = "ping"; // Gönderilecek mesaj
                    byte[] data = Encoding.ASCII.GetBytes(message); // Mesajý byte dizisine dönüþtür
                    NetworkStream stream = client.GetStream(); // Að akýþýný al

                    DateTime startTime = DateTime.Now; // Ping iþleminin baþlangýç zamaný
                    stream.Write(data, 0, data.Length); // Mesajý sunucuya gönder

                    byte[] buffer = new byte[256]; // Sunucudan gelen yanýtý saklamak için bir buffer (tampon) oluþtur
                    int bytesRead = stream.Read(buffer, 0, buffer.Length); // Sunucudan gelen yanýtý oku
                    string response = Encoding.ASCII.GetString(buffer, 0, bytesRead); // Byte dizisini string'e dönüþtür

                    TimeSpan responseTime = DateTime.Now - startTime; // Ping iþleminin toplam süresini hesapla

                    rtbResults.AppendText($"TCP Ping Baþarýlý!\n"); // Sonuçlarý RichTextBox'a ekle
                    rtbResults.AppendText($"Sunucu Yanýtý: {response}\n"); // Sunucudan gelen yanýtý göster
                    rtbResults.AppendText($"Gecikme: {responseTime.TotalMilliseconds} ms\n\n"); // Gecikme süresini göster
                }
                else
                {
                    rtbResults.AppendText($"Hata: Sunucuya baðlanýlamadý.\n"); // Baðlantý baþarýsýz olursa hata mesajý göster
                }
            }
            catch (Exception ex)
            {
                rtbResults.AppendText($"Hata: {ex.Message}\n"); // Herhangi bir hata olursa hata mesajýný göster
            }
        }

        private void btnUDPPing_Click_1(object sender, EventArgs e)
        {
            try
            {
                string serverIP = "10.11.9.5"; // Sunucunun IP adresi
                int port = 8080; // Sunucunun dinlediði port numarasý

                UdpClient client = new UdpClient(); // UdpClient nesnesi oluþturulur
                client.Client.ReceiveTimeout = 5000; // 5 saniyelik timeout süresi belirlenir

                string message = "ping"; // Gönderilecek mesaj
                byte[] data = Encoding.ASCII.GetBytes(message); // Mesajý byte dizisine dönüþtür

                DateTime startTime = DateTime.Now; // Ping iþleminin baþlangýç zamaný
                client.Send(data, data.Length, serverIP, port); // Mesajý sunucuya gönder

                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, port); // Sunucudan gelen yanýtý almak için IPEndPoint oluþtur

                try
                {
                    byte[] receivedData = client.Receive(ref remoteEP); // Sunucudan gelen yanýtý al (timeout uygulanýr)
                    string response = Encoding.ASCII.GetString(receivedData); // Byte dizisini string'e dönüþtür

                    DateTime endTime = DateTime.Now; // Ping iþleminin bitiþ zamaný
                    TimeSpan responseTime = endTime - startTime; // Ping iþleminin toplam süresini hesapla

                    rtbResults.AppendText($"UDP Ping Baþarýlý!\n"); // Sonuçlarý yazdýr
                    rtbResults.AppendText($"Sunucu Yanýtý: {response}\n");
                    rtbResults.AppendText($"Gecikme: {responseTime.TotalMilliseconds} ms\n\n");
                }
                catch (SocketException ex) when (ex.SocketErrorCode == SocketError.TimedOut)
                {
                    // Timeout oluþursa bu blok çalýþýr
                    rtbResults.AppendText("UDP Hata: Yanýt alýnamadý (timeout - 5 saniye içinde yanýt gelmedi).\n\n");
                }

                client.Close(); // UDP istemcisini kapat
            }
            catch (Exception ex)
            {
                rtbResults.AppendText($"Hata: {ex.Message}\n"); // Diðer hatalar burada yakalanýr
            }
        }
    }
}
