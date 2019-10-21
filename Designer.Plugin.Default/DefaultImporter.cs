﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Designer.Model;
using Designer.ViewModels;
using ExtendedXmlSerializer.Configuration;
using ExtendedXmlSerializer.ExtensionModel;
using ExtendedXmlSerializer.ExtensionModel.Content;
using ExtendedXmlSerializer.ExtensionModel.Xml;

namespace Designer.Plugin.Default
{
    public class DefaultImporter : ImportPlugin
    {
        private readonly IExtendedXmlSerializer serializer;

        public DefaultImporter()
        {
            serializer = new ConfigurationContainer()
                .UseAutoFormatting()
                .UseOptimizedNamespaces()                
                .EnableParameterizedContent()
                //.Type<TextBox>().Member(x => x.Text).Verbatim().WithValidCharacters()
                .Register(ColorConverter.Default)
                .Create();
        }

        public override string FileExtension => ".suppa";
        public override Task<IList<Document>> Load(Stream stream)
        {
            var deserialize = serializer.Deserialize<IList<Document>>(stream);
            return Task.FromResult(deserialize);
        }

        public override Task Save(Stream stream, IList<Document> documents)
        {           
            serializer.Serialize(stream, documents);
            return Task.CompletedTask;
        }
    }
}
