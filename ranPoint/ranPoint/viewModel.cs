﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace ViewModel
{
    [Serializable]
    public class viewModel : notify_property
    {
        public viewModel()
        {
            _reminder = new StringBuilder();
            _ran = new Random();
            xl = 100;
            yl = 100;
            fileAddress = @"Y:\ran_ponit.txt";

            //Point p = new Point();
            //p.X = (double)ran.Next(xl * 100) / 100;
            //p.Y = (double)ran.Next(yl * 100) / 100;
            //p.X = ran.Next(xl);
            //p.Y = ran.Next(yl);
            //points.Add(p);
            //p = new Point();
            //double num;
            //num = 0.1;
            //while (!integer(num))
            //{
            //p.X = (double)ran.Next(xl * 100) / 100;
            //p.Y = (double)ran.Next(yl * 100) / 100;
            //p.X = ran.Next(xl);
            //p.Y = ran.Next(yl);
            //while (p.X == points[0].X || p.Y == points[0].Y)
            //{
            //p.X = (double)ran.Next(xl * 100) / 100;
            //p.Y = (double)ran.Next(yl * 100) / 100;
            //        p.X = ran.Next(xl);
            //        p.Y = ran.Next(yl);
            //    }

            //    num = Math.Pow(points[0].X - p.X , 2) + Math.Pow(points[0].Y - p.Y , 2);
            //    num = Math.Sqrt(num);
            //}
            //points.Add(p);
            //write("(" + points[0].X.ToString() + "," + points[0].Y.ToString() + ")\r\n");
            //write("(" + points[1].X.ToString() + "," + points[1].Y.ToString() + ")\r\n");
            //reminder += "(" + points[0].X.ToString() + "," + points[0].Y.ToString() + ")\r\n";
            //reminder += "(" + points[1].X.ToString() + "," + points[1].Y.ToString() + ")\r\n";

            _cancel = true;
        }
        public string reminder
        {
            set
            {
                _reminder.Clear();
                _reminder.Append(value);
                OnPropertyChanged("reminder");
            }
            get
            {
                return _reminder.ToString();
            }
        }

        public string fileAddress
        {
            set
            {
                _fileAddress = value;
                OnPropertyChanged();
            }
            get
            {
                return _fileAddress;
            }
        }

        private Random ran
        {
            set
            {
                _ran = value;
            }
            get
            {
                return _ran;
            }
        }

        public int xl
        {
            set
            {
                _xl = value;
                OnPropertyChanged();
            }
            get
            {
                return _xl;
            }
        }

        public int yl
        {
            set
            {
                _yl = value;
                OnPropertyChanged();
            }
            get
            {
                return _yl;
            }
        }

        public bool _cancel;

        public int name
        {
            set
            {
                value = 0;
            }
            get
            {
                return points.Count + 1;
            }
        }

        public void coordinate()
        {
            _cancel = false;

            List<Point> satisfy;
            Point p = new Point();
            bool pointnull;
            bool finish;
            double num;

            //p.X = (double)ran.Next(xl * 100) / 100;
            //p.Y = (double)ran.Next(yl * 100) / 100;
            p.X = ran.Next(xl);
            p.Y = ran.Next(yl);
            points.Add(p);
            p = new Point();
            reminder = "";
            num = 0.1;
            while (!integer(num))
            {
                //p.X = (double)ran.Next(xl * 100) / 100;
                //p.Y = (double)ran.Next(yl * 100) / 100;
                p.X = ran.Next(xl);
                p.Y = ran.Next(yl);
                while (p.X == points[0].X || p.Y == points[0].Y)
                {
                    //p.X = (double)ran.Next(xl * 100) / 100;
                    //p.Y = (double)ran.Next(yl * 100) / 100;
                    p.X = ran.Next(xl);
                    p.Y = ran.Next(yl);
                }

                num = Math.Pow(points[0].X - p.X , 2) + Math.Pow(points[0].Y - p.Y , 2);
                num = Math.Sqrt(num);
            }
            points.Add(p);
            //write("(" + points[0].X.ToString() + "," + points[0].Y.ToString() + ")\r\n");
            //write("(" + points[1].X.ToString() + "," + points[1].Y.ToString() + ")\r\n");
            reminder += "(" + points[0].X.ToString() + "," + points[0].Y.ToString() + ")\r\n";
            reminder += "(" + points[1].X.ToString() + "," + points[1].Y.ToString() + ")\r\n";

            p.X = 0;
            p.Y = 0;
            pointnull = false;
            while (!pointnull)
            {
                satisfy = new List<Point>();
                for (int i = 0; i < xl; i++)
                {
                    for (int j = 0; j < yl; j++)
                    {
                        p.X = i;
                        p.Y = j;
                        finish = true;
                        foreach (Point t in points)
                        {
                            if (p.X == t.X && p.Y == t.Y)
                            {
                                finish = false;
                                break;
                            }
                            num = Math.Pow(t.X - p.X , 2) + Math.Pow(t.Y - p.Y , 2);
                            num = Math.Sqrt(num);
                            if (!integer(num))
                            {
                                finish = false;
                                break;
                            }
                        }
                        if (finish)
                        {
                            satisfy.Add(p);
                            p = new Point();
                        }
                    }
                }
                if (satisfy.Count > 0)
                {
                    p = satisfy[ran.Next(satisfy.Count)];
                    points.Add(p);
                    //write("(" + p.X.ToString() + "," + p.Y.ToString() + ")\r\n");
                    reminder += "(" + p.X.ToString() + "," + p.Y.ToString() + ")\r\n";
                }
                else
                {
                    reminder += "没有点";
                    pointnull = true;
                    string temp;
                    temp = fileAddress;
                    fileAddress = fileAddress.Substring(0 , fileAddress.Length - 4);
                    fileAddress += DateTime.Now.Hour.ToString();
                    fileAddress += DateTime.Now.Minute.ToString();
                    fileAddress += DateTime.Now.Second.ToString();
                    fileAddress = fileAddress + ".txt";
                    StringBuilder str = new StringBuilder();
                    foreach (var t in points)
                    {
                        str.Append("(" + t.X.ToString() + "," + t.Y.ToString() + ")\r\n");
                    }
                    write(str.ToString());
                    points = new List<Point>();
                    fileAddress = temp;
                }
            }


            //Point p = new Point();
            //bool finish;
            //double num;
            ////int[] x = new int[xl];
            ////int[] y = new int[yl];
            ////for (int i = 0; i < xl; i++)
            ////{
            ////    x[i] = 0;
            ////}
            ////for (int i = 0; i < yl; i++)
            ////{
            ////    y[i] = 0;
            ////}

            ////foreach (Point t in points)
            ////{
            ////    x[(int)t.X]++;
            ////    y[(int)t.Y]++;
            ////}

            //num = 0;
            //finish = false;
            //while (!finish)
            //{
            //    finish = true;
            //    p.X = (double)ran.Next(xl * 100) / 100;
            //    p.Y = (double)ran.Next(yl * 100) / 100;
            //    //while (x[(int)p.X] > 3)
            //    //{
            //    //    p.X = ran.Next(xl);
            //    //}
            //    //while (y[(int)p.Y] > 3)
            //    //{
            //    //    p.X = ran.Next(xl);
            //    //}

            //    foreach (Point t in points)
            //    {
            //        if (p.X == t.X && p.Y == t.Y)
            //        {
            //            finish = false;
            //            break;
            //        }
            //        num = Math.Pow(t.X - p.X , 2) + Math.Pow(t.Y - p.Y , 2);
            //        num = Math.Sqrt(num);
            //        if (!integer(num))
            //        {
            //            finish = false;
            //            break;
            //        }
            //    }
            //}

            _cancel = true;
        }

        private void write(string str)
        {
            FileStream file = new FileStream(fileAddress , FileMode.Append);
            byte[] buf;
            buf = Encoding.Unicode.GetBytes(str);
            file.Write(buf , 0 , buf.Length);
            file.Flush();
            file.Close();
        }

        private void read()
        {
            //序列化退出 [Serializable]   
            if (!( points.Count == 0 ))
            {
                using (FileStream file = new FileStream(fileAddress , FileMode.Create , FileAccess.Write))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<Point>));
                    xml.Serialize(file , points);
                }
            }
            else
            {
                using (FileStream file = new FileStream(fileAddress , FileMode.Open , FileAccess.Read))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<Point>));
                    points = (List<Point>)xml.Deserialize(file);
                }
            }
        }

        private bool integer(double num)
        {
            double decimalnum = 0.000000001;
            int n;
            n = (int)num;
            if (n - decimalnum < num && n + decimalnum > num)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string _fileAddress;
        private StringBuilder _reminder;
        private Random _ran;
        private int _xl;
        private int _yl;
        private List<Point> points = new List<Point>();

    }
}
