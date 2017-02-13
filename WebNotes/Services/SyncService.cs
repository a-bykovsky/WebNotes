using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using WebNotes.EF;
using WebNotes.Models;
using WebNotes.Repositories;

namespace WebNotes.Services
{
    public class SyncService
    {
        private string syncUrl = "http://profigroup.by/applicants-tests/mobile/v2/OetgnK4/";
        private INoteRepository noteRepository;

        public SyncService(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public void Sync()
        {
            try
            {
                var serverNotes = this.Get() ?? new List<Note>();
                var localNotes = noteRepository.GetAll();

                var mergedList = localNotes.Union(serverNotes).ToList();
                Put(mergedList);
                var exceptList = mergedList.Except(localNotes).ToList();
                exceptList.ForEach(n => noteRepository.Insert(n));
                noteRepository.Save();
            }
            catch
            {
                // Sync Failed
            }
            
        }

        public void ToServer()
        {
            try
            {
                var localNotes = noteRepository.GetAll();
                Put(localNotes.ToList());
            }
            catch
            {
                //Sync Failed
            }
        }

        private List<Note> Get()
        {            
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(syncUrl);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            string text;

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                text = stream.ReadToEnd();
            }
                
            return JsonConvert.DeserializeObject<List<Note>>(text);
        }

        private void Put(List<Note> notes)
        {
            WebRequest request = WebRequest.Create(syncUrl);
            request.Method = "PUT";
            string data = JsonConvert.SerializeObject(notes);
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine(responseFromServer);
            reader.Close();
            dataStream.Close();
            response.Close();
        }

    }
}