using System.Net.Sockets; // A� ileti�imi i�in gerekli s�n�flar� i�erir (TcpClient, UdpClient, vb.)
using System.Net;         // IP adresleri ve a� ileti�imi i�in gerekli s�n�flar� i�erir (IPAddress, IPEndPoint, vb.)
using System.Text;        // Metin kodlama ve d�n��t�rme i�lemleri i�in gerekli s�n�flar� i�erir (Encoding, vb.)
using System;             // Temel sistem i�lemleri ve hata yakalama i�in gerekli

namespace UDP_TCP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); // Form bile�enlerini ba�lat�r (Windows Forms tasar�m� i�in otomatik olu�turulur)
        }

        private void btnTCPPing_Click(object sender, EventArgs e)
        {
            try
            {
                // Telefonunuzdaki sunucu bilgileri
                string serverIP = "ip giriniz �rnek: 12.168.154.6"; // Sunucunun IP adresi (yerel a�da)
                int port = 7070; // portunuzu giriniz �rnek 7070; // Sunucunun dinledi�i port numaras�

                // TCP istemci olu�tur ve ba�lan
                using TcpClient client = new TcpClient(); // TcpClient nesnesi olu�turulur
                client.ConnectAsync(serverIP, port).Wait(5000); // Sunucuya asenkron olarak ba�lan�r ve 5 saniye bekler

                if (client.Connected) // Ba�lant� ba�ar�l�ysa
                {
                    string message = "ping"; // G�nderilecek mesaj
                    byte[] data = Encoding.ASCII.GetBytes(message); // Mesaj� byte dizisine d�n��t�r
                    NetworkStream stream = client.GetStream(); // A� ak���n� al

                    DateTime startTime = DateTime.Now; // Ping i�leminin ba�lang�� zaman�
                    stream.Write(data, 0, data.Length); // Mesaj� sunucuya g�nder

                    byte[] buffer = new byte[256]; // Sunucudan gelen yan�t� saklamak i�in bir buffer (tampon) olu�tur
                    int bytesRead = stream.Read(buffer, 0, buffer.Length); // Sunucudan gelen yan�t� oku
                    string response = Encoding.ASCII.GetString(buffer, 0, bytesRead); // Byte dizisini string'e d�n��t�r

                    TimeSpan responseTime = DateTime.Now - startTime; // Ping i�leminin toplam s�resini hesapla

                    rtbResults.AppendText($"TCP Ping Ba�ar�l�!\n"); // Sonu�lar� RichTextBox'a ekle
                    rtbResults.AppendText($"Sunucu Yan�t�: {response}\n"); // Sunucudan gelen yan�t� g�ster
                    rtbResults.AppendText($"Gecikme: {responseTime.TotalMilliseconds} ms\n\n"); // Gecikme s�resini g�ster
                }
                else
                {
                    rtbResults.AppendText($"Hata: Sunucuya ba�lan�lamad�.\n"); // Ba�lant� ba�ar�s�z olursa hata mesaj� g�ster
                }
            }
            catch (Exception ex)
            {
                rtbResults.AppendText($"Hata: {ex.Message}\n"); // Herhangi bir hata olursa hata mesaj�n� g�ster
            }
        }

        private void btnUDPPing_Click_1(object sender, EventArgs e)
        {
            try
            {
                string serverIP = "10.11.9.5"; // Sunucunun IP adresi
                int port = 8080; // Sunucunun dinledi�i port numaras�

                UdpClient client = new UdpClient(); // UdpClient nesnesi olu�turulur
                client.Client.ReceiveTimeout = 5000; // 5 saniyelik timeout s�resi belirlenir

                string message = "ping"; // G�nderilecek mesaj
                byte[] data = Encoding.ASCII.GetBytes(message); // Mesaj� byte dizisine d�n��t�r

                DateTime startTime = DateTime.Now; // Ping i�leminin ba�lang�� zaman�
                client.Send(data, data.Length, serverIP, port); // Mesaj� sunucuya g�nder

                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, port); // Sunucudan gelen yan�t� almak i�in IPEndPoint olu�tur

                try
                {
                    byte[] receivedData = client.Receive(ref remoteEP); // Sunucudan gelen yan�t� al (timeout uygulan�r)
                    string response = Encoding.ASCII.GetString(receivedData); // Byte dizisini string'e d�n��t�r

                    DateTime endTime = DateTime.Now; // Ping i�leminin biti� zaman�
                    TimeSpan responseTime = endTime - startTime; // Ping i�leminin toplam s�resini hesapla

                    rtbResults.AppendText($"UDP Ping Ba�ar�l�!\n"); // Sonu�lar� yazd�r
                    rtbResults.AppendText($"Sunucu Yan�t�: {response}\n");
                    rtbResults.AppendText($"Gecikme: {responseTime.TotalMilliseconds} ms\n\n");
                }
                catch (SocketException ex) when (ex.SocketErrorCode == SocketError.TimedOut)
                {
                    // Timeout olu�ursa bu blok �al���r
                    rtbResults.AppendText("UDP Hata: Yan�t al�namad� (timeout - 5 saniye i�inde yan�t gelmedi).\n\n");
                }

                client.Close(); // UDP istemcisini kapat
            }
            catch (Exception ex)
            {
                rtbResults.AppendText($"Hata: {ex.Message}\n"); // Di�er hatalar burada yakalan�r
            }
        }
    }
}
