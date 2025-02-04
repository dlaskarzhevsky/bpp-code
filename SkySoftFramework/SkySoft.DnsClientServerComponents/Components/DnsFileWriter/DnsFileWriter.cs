namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Provides DNS file writer functionality
    /// </summary>
    public partial class DnsFileWriter : SkySoft.BPPApplication.RequestHandler
    {
        #region Overridden Methods
        /// <summary>
        /// Handles request
        /// </summary>
        protected override void HandleRequest()
        {
            VerifyThatPathToDnsRecordsFileContainsDirectoryName();
            if (DnsRecordsFileExists)
            {
                LoadListOfDnsRecordsFromFile();
                FindDnsRecordByApplicationLayerFullNameInListOfDnsRecordsLoadedFromFile();
                if (DnsRecordFound)
                {
                    UpdateDnsRecordData();
                    SaveUpdatedDataIntoFile();
                }
                else
                {
                    CreateNewDnsRecord();
                    AddDnsRecordToListOfDnsRecords();
                    SaveUpdatedDataIntoFile();
                }
            }
            else
            {
                CreateEmptyListOfDnsRecords();
                CreateNewDnsRecord();
                AddDnsRecordToListOfDnsRecords();
                SaveUpdatedDataIntoFile();
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public override void ReleaseResources()
        {
            ListOfDnsRecordsFromFile = null;
            base.ReleaseResources();
        }

        /// <summary>
        /// Validates component
        /// </summary>
        protected override void ValidateComponent()
        {
            if (DnsRecordDTO == null)
            {
                ComponentIsValid = false;
            }
        }
        #endregion
    }
}
