﻿namespace LiteDbExplorer.Core
{

    extern alias v4;
    using LiteDBv4 = v4::LiteDB;

    public sealed class FileCollectionReference : CollectionReference
    {
        public FileCollectionReference(string name, DatabaseReference database) : base(name, database)
        {
        }

        public override void RemoveItem(DocumentReference document)
        {
            Database.LiteDatabase.FileStorage.Delete(document.LiteDocument["_id"]);
            Items.Remove(document);
        }

        public DocumentReference AddFile(string id, string path)
        {
            var file = Database.LiteDatabase.FileStorage.Upload(id, path);
            var newDoc = new DocumentReference(file.AsDocument, this);
            Items.Add(newDoc);
            return newDoc;
        }

        public void SaveFile(DocumentReference document, string path)
        {
            var file = GetFileObject(document);
            file.SaveAs(path);
        }

        public LiteDBv4.LiteFileInfo GetFileObject(DocumentReference document)
        {
            return Database.LiteDatabase.FileStorage.FindById(document.LiteDocument["_id"]);
        }
    }
}