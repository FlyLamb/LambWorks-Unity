

using System;
namespace LambWorks.Networking {
    public interface IMetadataIO {
        void IOWriteMetadata(string meta, object data, TransportType transport = TransportType.dummy);
        object IOReadMetadata(string meta);

        Action IOGotData { get; set; }

    }

}