using DmailApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DmailApp.Presentation.Actions.ProfileSettingsActions
{
    public class ProfileSettingsAction : BaseMenuAction
    {
        public ProfileSettingsAction(IList<IAction> actions) : base(actions)
        {
            Name = "Profile";
        }
    }
}
