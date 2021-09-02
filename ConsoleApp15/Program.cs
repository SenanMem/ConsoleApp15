using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp15
{
    public class ListNode:IEnumerable
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }

        public IEnumerator GetEnumerator()
        {
            yield return this;
        }
        public override string ToString()
        {
            return this.val.ToString();
        }
    }
    public static class Solution
    {
        static ListNode vs = new ListNode();
        static ListNode temp = vs;
        public static ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            if (l1 == null)
            {
                vs.next = l2;
                return vs;
            }
            else if (l2 == null)
            {
                vs.next = l1;
                return vs;
            }
            else if (l1.val >= l2.val)
            {
                vs.next = l2;
                vs = vs.next;
                MergeTwoLists(l1, l2.next);
            }
            else if (l1.val < l2.val)
            {
                vs.next = l1;
                vs = vs.next;
                MergeTwoLists(l1.next, l2);
            }
            return temp.next;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //SocketInformation socketInformation = new SocketInformation()
            //{
            //    Options-new SocketInformationOptions();

            //};
            int port = 8005;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"),port);

            using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // & | ~ ^
             
            try
            {
                socket.Bind(endPoint);
                socket.Listen(10);
                Console.WriteLine("Server is up, waiting for connections");
                while (true)
                {
                    Socket handler = socket.Accept();

                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] dataBuffer = new byte[256];
                    do
                    {
                        bytes = handler.Receive(dataBuffer);
                        builder.Append(Encoding.UTF8.GetString(dataBuffer, 0, bytes));
                    } while (handler.Available > 0);

                    Console.WriteLine(DateTime.Now.ToShortTimeString() + " : " + builder.ToString());

                    string message = "Your message was delivered!";
                    dataBuffer = Encoding.UTF8.GetBytes(message);

                    handler.Send(dataBuffer);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close(); 
                
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }




           // var l1 = new ListNode(1);
           // var l12 = new ListNode(1);
           // l1.next = l12;
           // var l13 = new ListNode(2);
           // l12.next = l13;
           // var l14 = new ListNode(12);
           // l13.next = l14;
           // var l2 = new ListNode(5);
           // var l21 = new ListNode(6);
           // l2.next = l21;
           // var l22 = new ListNode(8);
           // l21.next = l22;
           // var l23 = new ListNode(13);
           // l22.next = l23;
           // var l24 = new ListNode(15);
           // l23.next = l24;
           //var temp =Solution.MergeTwoLists(l1, l2);
           // foreach (var item in temp)
           // {
           //     Console.WriteLine(item.ToString());
           // }
        }
    }
}
