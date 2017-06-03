using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetStandardSpotifyWebApi.ObjectModel
{
    public sealed class RepeatEnum {

        public int Value { get; }
        public string Name { get; }
        public string Description { get; }


        #region Enum Stuff
        private static Dictionary<int, RepeatEnum> _typesById { get; } = new Dictionary<int, RepeatEnum>();
        private static Dictionary<string, RepeatEnum> _typesByName { get; } = new Dictionary<string, RepeatEnum>();
        private static int currentId { get; set; } = -1;

        private RepeatEnum(int value, string name) {
            this.Name = name;
            this.Value = value;
            _typesById.Add(value, this);
            _typesByName.Add(name, this);
            currentId += 1;
        }

        private RepeatEnum(string name, string description) {
            this.Name = name;
            this.Value = currentId;
            this.Description = description;
            _typesById.Add(this.Value, this);
            _typesByName.Add(name, this);
            currentId += 1;
        }

        public static RepeatEnum fromInt(int value) {
            return _typesById[value];
        }

        public static RepeatEnum fromName(string name) {
            return _typesByName[name];
        }


        public override string ToString() {
            return Name;
        }
        #endregion

        #region list of Repeat Enums
        public readonly static RepeatEnum OFF = new RepeatEnum("off", "Turns repeat off");
        public readonly static RepeatEnum CONTEXT = new RepeatEnum("context", "Repeats the current context");
        public readonly static RepeatEnum TRACK = new RepeatEnum("track", "Repeats the current track");

        #endregion

    }
}
