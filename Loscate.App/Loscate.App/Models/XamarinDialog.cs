using Loscate.DTO.Social;

namespace Loscate.App.Models
{
    public class XamarinDialog : Dialog
    {
        public XamarinUser XamarinCompanion { get; set; }

        public XamarinDialog(Dialog dialog)
        {
            Companion = dialog.Companion;
            XamarinCompanion = new XamarinUser(Companion);
            this.LastMessage = dialog.LastMessage;
        }
    }
}