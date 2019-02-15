using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrderEntrySystem
{
    /// <summary>
    /// The class representing a view model for commands.
    /// </summary>
    public class CommandViewModel : ViewModel
    {
        /// <summary>
        /// The auto property that represents the group name.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="displayName">The name of the command.</param>
        /// <param name="command">The logic of the command</param>
        public CommandViewModel(string displayName, ICommand command, string groupName)
            : this(displayName, command, false, false, groupName)
        {
        }

        public CommandViewModel(string displayName, ICommand command, bool isDefault, bool isCancel, string groupName)
            : base(displayName)
        {
            if (command == null)
            {
                throw new Exception("Command was null.");
            }

            this.Command = command;
            this.IsCancel = isCancel;
            this.IsDefault = isDefault;
            this.GroupName = groupName;
        }

        /// <summary>
        /// Gets the command to be viewed.
        /// </summary>
        public ICommand Command { get; private set; }

        public bool IsDefault { get; private set; }

        public bool IsCancel { get; private set; }
    }
}