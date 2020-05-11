namespace Xml
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using Data;

    public class XmlDataLoader
    {
        public const string AssetPath = @"C:\Users\User\Projects\#lab\lab5\assets";

        public Project ToProject(XElement element)
        {
            var workers = new List<Worker>();
            var serializer = SetUpSerializer(workers);
            var project = (Project) serializer.Deserialize(element.CreateReader());
            project.Workers = workers;
            return project;
        }
        
        public Project FromXml(int id)
        {
            using (var fs = new FileStream($"{AssetPath}/projects/{id}.xml", FileMode.Open))
            {
                var workers = new List<Worker>();
                var serializer = SetUpSerializer(workers);
                var project = (Project) serializer.Deserialize(fs);
                project.Workers = workers;
                return project;
            }
        }
        
        private XmlSerializer SetUpSerializer(List<Worker> workers)
        {
            var serializer = new XmlSerializer(typeof(Project));
            serializer.UnknownElement += (_, args) => ProcessWorker(workers, args);
            return serializer;
        }
        
        private void ProcessWorker(List<Worker> workers, XmlElementEventArgs e)
        {
            if (e.Element.Name == "Worker" && int.TryParse(e.Element.InnerText, out var workerId))
            {
                workers.Add(ToWorker(workerId));
            }
        }
        
        public Worker ToWorker(int id)
        {
            var serializer = new XmlSerializer(typeof(Worker));
            using (var fs = new FileStream($"{AssetPath}/workers/{id}.xml", FileMode.Open))
            {
                var worker = (Worker) serializer.Deserialize(fs);
                return worker;
            }
        }
        
        public Worker ToWorker(XElement worker)
        {
            var serializer = new XmlSerializer(typeof(Worker));
            return (Worker) serializer.Deserialize(worker.CreateReader());
        }
        
        public void ToXml(Project project)
        {
            var serializer = new XmlSerializer(typeof(Project));
            using (var writer = new StreamWriter($"{AssetPath}/projects/{project.Id}.xml"))
            {
                serializer.Serialize(writer, project);
            }
            var xmlDoc = new XmlDocument();
            var xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            var root = xmlDoc.DocumentElement;
            xmlDoc.InsertBefore(xmlDeclaration, root);
            var body = xmlDoc.CreateElement(string.Empty, "Project", string.Empty);
            xmlDoc.AppendChild(body);
            
            var id = xmlDoc.CreateElement("Id");
            id.InnerText = project.Id.ToString();
            body.AppendChild(id);
            
            var name = xmlDoc.CreateElement("Name");
            name.InnerText = project.Name;
            body.AppendChild(name);
            
            var cost = xmlDoc.CreateElement("Cost");
            cost.InnerText = project.Cost.ToString(CultureInfo.InvariantCulture);
            body.AppendChild(cost);
            
            var start = xmlDoc.CreateElement("Start");
            start.InnerText = XmlConvert.ToString(project.Start, XmlDateTimeSerializationMode.Utc);
            body.AppendChild(start);
            
            var end = xmlDoc.CreateElement("End");
            end.InnerText = XmlConvert.ToString(project.End, XmlDateTimeSerializationMode.Utc);
            body.AppendChild(end);
            
            var isCompleted = xmlDoc.CreateElement("IsCompleted");
            isCompleted.InnerText = project.IsCompleted.ToString().ToLower();
            body.AppendChild(isCompleted);
            
            var workers = xmlDoc.CreateElement("Workers");
            body.AppendChild(workers);
            
            foreach (var worker in project.Workers)
            {
                var w = xmlDoc.CreateElement("Worker");
                w.InnerText = worker.Id.ToString();
                workers.AppendChild(w);
            }
            
            xmlDoc.Save($"{AssetPath}/projects/{project.Id}.xml");
        }
        
        public async void ToXml(Worker worker)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                NewLineOnAttributes = true,
                Async = true
            };
            using (var writer = XmlWriter.Create($"{AssetPath}/workers/{worker.Id}.xml", settings))
            {
                await writer.WriteStartDocumentAsync();
                await writer.WriteStartElementAsync(null, "Worker", null);
                
                await writer.WriteStartElementAsync(null, "Id", null);
                await writer.WriteStringAsync(worker.Id.ToString());
                await writer.WriteEndElementAsync();
                
                await writer.WriteStartElementAsync(null, "FirstName", null);
                await writer.WriteStringAsync(worker.FirstName);
                await writer.WriteEndElementAsync();
                
                await writer.WriteStartElementAsync(null, "SecondName", null);
                await writer.WriteStringAsync(worker.SecondName);
                await writer.WriteEndElementAsync();
                
                await writer.WriteStartElementAsync(null, "Role", null);
                await writer.WriteStringAsync(worker.Role);
                await writer.WriteEndElementAsync();
                
                await writer.WriteEndElementAsync();
                await writer.WriteEndDocumentAsync();
                await writer.FlushAsync();  
            }
        }
    }
}