using System.Collections.Generic;
using PotatoTranslationControl.Core;

namespace PotatoTranslationControl.Editor
{
    public class TranslationRecordsConverter
    {
        public List<TranslationListRecord> ConvertRecords(List<TranslationRecord> records)
        {
            List<TranslationListRecord> listRecords = new List<TranslationListRecord>();

            for(int i = 0; i < records.Count; i++)
            {
                listRecords.Add(new TranslationListRecord(i.ToString(), records[i].Key, records[i].Value));
            }

            return listRecords;
        }

        public List<TranslationListRecord> ConvertRecordsWithAddingEmptyRecord(List<TranslationRecord> records)
        {
            List<TranslationListRecord> listRecords = ConvertRecords(records);
            TranslationListRecord emptyRecord = new TranslationListRecord(listRecords.Count.ToString(), "", "");

            listRecords.Add(emptyRecord);

            return listRecords;
        }
    }
}
