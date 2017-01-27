/* ZerochanParser - Parser for zerochan
 * Remember to credit me after using this in your app: 
 * Piotr "MiXer" Mikstacki => ja.to.mixer@gmail.com
 * */
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;
using System.ComponentModel;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ZerochanParser
{
    public class Parser
    {
        public List<string> AnimeNumbers = new List<string>();
        public int curProgress = 0;
        public int ToDownload = 0;

        public void GetImage(string AnimeName, string number)
        {

            WebClient webClient = new WebClient();
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\" + AnimeName);
            webClient.DownloadProgressChanged += (object sender, DownloadProgressChangedEventArgs e) =>
            {
                MainWindow.instance.progress.Content = string.Format("{0} / {1}", curProgress, ToDownload);
                //MessageBox.Show(string.Format("http://static.zerochan.net/{0}.240.{1}.jpg", AnimeName, number));
            };
            webClient.DownloadFileCompleted += (object sender, AsyncCompletedEventArgs e) => {
                curProgress++;
                MainWindow.instance.progress.Content = string.Format("{0} / {1}", curProgress, ToDownload);

            };
            webClient.DownloadFileAsync(new Uri(string.Format("http://static.zerochan.net/{0}.full.{1}.jpg", AnimeName, number)), Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\" + AnimeName + "\\" + AnimeName+"_" + number + ".jpg");
        }

        public async void ParseAnimeNumber(string AnimeName)
        {
            curProgress = 0;
            ToDownload = 0;
            try
            {

                HttpClient http = new HttpClient();
                var response = await http.GetByteArrayAsync(new Uri(string.Format("http://zerochan.net/{0}", AnimeName.Replace(' ', '+'))));
                String source = Encoding.GetEncoding("UTF-8").GetString(response, 0, response.Length - 1);
                source = WebUtility.HtmlDecode(source);
                HtmlDocument resultat = new HtmlDocument();
                resultat.LoadHtml(source);
                HtmlNode ul = resultat.GetElementbyId("thumbs2");
                HtmlNodeCollection childList = ul.ChildNodes;
                foreach (HtmlNode n in childList)
                {
                    HtmlNodeCollection il = n.ChildNodes;
                    foreach (HtmlNode l in il)
                    {
                        if (l.HasAttributes)
                        {
                            List<char> chars = l.Attributes[0].Value.ToString().ToList<char>();
                            chars.RemoveAt(0);
                            string s = new string(chars.ToArray());
                            AnimeNumbers.Add(s);
                            GetImage(AnimeName, s);
                            ToDownload++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Anime not found");
            }
           
           
        }
        public async void ParseAnimeNumber(string AnimeName, string Character)
        {
            curProgress = 0;
            ToDownload = 0;
            try
            {
                HttpClient http = new HttpClient();

                var response = await http.GetByteArrayAsync(new Uri(string.Format("http://zerochan.net/{0}", Character.Replace(' ', '+'))));
                String source = Encoding.GetEncoding("UTF-8").GetString(response, 0, response.Length - 1);
                source = WebUtility.HtmlDecode(source);
                HtmlDocument resultat = new HtmlDocument();
                resultat.LoadHtml(source);
                HtmlNode ul = resultat.GetElementbyId("thumbs2");
                HtmlNodeCollection childList = ul.ChildNodes;
                foreach (HtmlNode n in childList)
                {
                    HtmlNodeCollection il = n.ChildNodes;
                    foreach (HtmlNode l in il)
                    {
                        if (l.HasAttributes)
                        {
                            List<char> chars = l.Attributes[0].Value.ToString().ToList<char>();
                            chars.RemoveAt(0);
                            string s = new string(chars.ToArray());
                            AnimeNumbers.Add(s);
                            GetImage(Character, s);
                            ToDownload++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Character Name!");
            }
        }
        public async void ParseAnimeNumber(string AnimeName, int page)
        {
            curProgress = 0;
            ToDownload = 0;
            try
            {
                for (int i = 1; i < page; i++)
                {
                    HttpClient http = new HttpClient();
                    var response = await http.GetByteArrayAsync(new Uri(string.Format("http://zerochan.net/{0}?p={1}", AnimeName.Replace(' ', '+'), i.ToString())));
                    String source = Encoding.GetEncoding("UTF-8").GetString(response, 0, response.Length - 1);
                    source = WebUtility.HtmlDecode(source);
                    HtmlDocument resultat = new HtmlDocument();
                    resultat.LoadHtml(source);
                    HtmlNode ul = resultat.GetElementbyId("thumbs2");
                    HtmlNodeCollection childList = ul.ChildNodes;
                    foreach (HtmlNode n in childList)
                    {
                        HtmlNodeCollection il = n.ChildNodes;
                        foreach (HtmlNode l in il)
                        {
                            if (l.HasAttributes)
                            {
                                List<char> chars = l.Attributes[0].Value.ToString().ToList<char>();
                                chars.RemoveAt(0);
                                string s = new string(chars.ToArray());
                                AnimeNumbers.Add(s);
                                GetImage(AnimeName, s);
                                ToDownload++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Page Limit probably overloaded");
            }


        }
        public async void ParseAnimeNumber(string AnimeName, string Character, string page)
        {
            curProgress = 0;
            ToDownload = 0;
            try
            {
                for (int i = 1; i < int.Parse(page); i++)
                {
                    HttpClient http = new HttpClient();

                    var response = await http.GetByteArrayAsync(new Uri(string.Format("http://zerochan.net/{0}?p={1}", Character.Replace(' ', '+'), i.ToString())));
                    String source = Encoding.GetEncoding("UTF-8").GetString(response, 0, response.Length - 1);
                    source = WebUtility.HtmlDecode(source);
                    HtmlDocument resultat = new HtmlDocument();
                    resultat.LoadHtml(source);
                    HtmlNode ul = resultat.GetElementbyId("thumbs2");
                    HtmlNodeCollection childList = ul.ChildNodes;
                    foreach (HtmlNode n in childList)
                    {
                        HtmlNodeCollection il = n.ChildNodes;
                        foreach (HtmlNode l in il)
                        {
                            if (l.HasAttributes)
                            {
                                List<char> chars = l.Attributes[0].Value.ToString().ToList<char>();
                                chars.RemoveAt(0);
                                string s = new string(chars.ToArray());
                                AnimeNumbers.Add(s);
                                GetImage(Character, s);
                                ToDownload++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Character Name!");
            }
        }

    }

}
