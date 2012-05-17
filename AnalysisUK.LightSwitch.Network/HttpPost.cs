using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.SPOT;

namespace AnalysisUK.LightSwitch.Network
{
    class HttpPost
    {
        public void Test()
        {
            string hostname = "members.bookswap.ws";
            string path = "http://members.bookswap.ws/Content/AddItem.php";
        }

        private SendMessage(IPAddress address, int port, string message)
        {
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
            SendMessage(address, port, messageBytes);
        }

        private byte[] SendMessage(IPAddress address, int port, byte[] message)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4))
            {
                EndPoint serverEndPoint = new IPEndPoint(address, port);
                socket.SendTo(message, serverEndPoint);

                byte[] receiveBuffer = new byte[1024];
                int receivedBytes = socket.Receive(receiveBuffer);
                Debug.Print("Received : " + receivedBytes + " bytes");
                //System.Text.Decoder decoder = new System.Text.Decoder();
                //decoder.Convert(receiveBuffer, 0, receivedBytes, 
                return receivedBytes;
            }
        }

        public void PostData(string hostname, int port, string path)
        {
            //path = "/servlet/SomeServlet";

            //hostname = "hostname.com";
            // TODO: Use DNS lookup.
            IPAddress address = IPAddress.Parse("174.129.41.182");

            try
            {
                // Construct data
                // TODO: UTF8 encode.
                string data = "key1 = value1";
                data += "&" + "key2 = value2";

                // Send header
                string content = "POST " + path + " HTTP/1.0\r\n";
                content += "Content-Length: " + data.Length + "\r\n";
                content += "Content-Type: application/x-www-form-urlencoded\r\n";
                content += "\r\n";
                SendMessage(address, port, content);
                
    
                    // Get response
                    BufferedReader rd = new BufferedReader(new InputStreamReader(socket.getInputStream()));
                    String line;
                    while ((line = rd.readLine()) != null)
                    {
                        // Process line...
                    }
                    wr.close();
                    rd.close();
                }
            }

        

    catch
                (Exception
                e)
                {
                }

            }
        
    }
}
