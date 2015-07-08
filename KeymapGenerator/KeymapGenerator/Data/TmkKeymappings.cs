using System.Collections.Generic;

namespace KeymapGenerator.Data
{
    public class TmkKeymappings
    {
        public static string GetDisplayText(string keypress)
        {
            return _mapDictionary.ContainsKey(keypress)
                ? _mapDictionary[keypress]
                : keypress;
        }

        public static Dictionary<string, string> MapDictionary
        {
            get { return _mapDictionary; }
            set { _mapDictionary = value; }
        }

        private static Dictionary<string, string> _mapDictionary = new Dictionary<string, string>
        {
            // Modifiers and Special Keys
            { "KC_PSCR", "Print\nScreen" },
            { "KC_SLCK", "Scroll\nLock" },
            { "KC_PAUS", "Pause\nBreak" },
            { "KC_MINS", "- _" },
            { "KC_EQL", "= +" },
            { "KC_GRV", "` ~" },
            { "KC_RBRC", "] }" },
            { "KC_LBRC", "[ {" },
            { "KC_COMM", ", <" },
            { "KC_DOT", ". >" },
            { "KC_BSLS", "\\" },
            { "KC_SLSH", "/ |" },
            { "KC_SCLN", "; :" },
            { "KC_QUOT", "' \"" },
            { "KC_NUHS", "Non-US\nHash" },
            { "KC_NUBS", "Non-US\nBackslash" },
            { "KC_LCAP", "Locking\nCaps" },
            { "KC_LNUM", "Locking\nNum" },
            { "KC_LSCR", "Locking\nScroll" },
            { "KC_ERAS", "Alt Erase" },
            { "KC_CLR", "Clear" },
            { "KC_ESC", "Escape" },
            { "KC_BSPC","Backspace" },
            { "KC_ENT", "Enter" },
            { "KC_DEL", "Delete" },
            { "KC_INS", "Insert" },
            { "KC_TAB", "Tab" },
            { "KC_CAPS", "Capslock" },
            { "KC_LCTRL", "L Ctrl" },
            { "KC_LCTL", "L Ctrl" },
            { "KC_LSHIFT", "L Shift" },
            { "KC_LSFT", "L Shift" },
            { "KC_LALT", "L Alt" },
            { "KC_LGUI", "L GUI" },
            { "KC_RCTRL", "R Ctrl" },
            { "KC_RCTL", "R Ctrl" },
            { "KC_RSHIFT", "R Shift" },
            { "KC_RSFT", "R Shift" },
            { "KC_RALT", "R Alt" },
            { "KC_RGUI", "R GUI" },
            { "KC_APP", "App" },
            { "KC_SPC", "Space" },
            { "KC_HOME", "Home" },
            { "KC_END", "End" },
            { "KC_PGUP", "Page Up" },
            { "KC_PGDOWN", "Page Down" },
            { "KC_RIGHT", "Arrow\nRight" },
            { "KC_RGHT", "Arrow\nRight" },
            { "KC_LEFT", "Arrow\nLeft" },           
            { "KC_DOWN", "Arrow\nDown" },
            { "KC_UP", "Arrow\nUp" },

            // Alphanumerics
            { "KC_A", "A" },
            { "KC_B", "B" },
            { "KC_C", "C" },
            { "KC_D", "D" },
            { "KC_E", "E" },
            { "KC_F", "F" },
            { "KC_G", "G" },
            { "KC_H", "H" },
            { "KC_I", "I" },
            { "KC_J", "J" },
            { "KC_K", "K" },
            { "KC_L", "L" },
            { "KC_M", "M" },             
            { "KC_N", "N" },
            { "KC_O", "O" },
            { "KC_P", "P" },
            { "KC_Q", "Q" },
            { "KC_R", "R" },
            { "KC_S", "S" },
            { "KC_T", "T" },
            { "KC_U", "U" },
            { "KC_V", "V" },
            { "KC_W", "W" },
            { "KC_X", "X" },
            { "KC_Y", "Y" },
            { "KC_Z", "Z" },
            { "KC_1", "1" },
            { "KC_2", "2" },
            { "KC_3", "3" },             
            { "KC_4", "4" },
            { "KC_5", "5" },
            { "KC_6", "6" },
            { "KC_7", "7" },
            { "KC_8", "8" },
            { "KC_9", "9" },
            { "KC_0", "0" },

            // Function keys
            { "KC_F1", "F1" },
            { "KC_F2", "F2" },
            { "KC_F3", "F3" },
            { "KC_F4", "F4" },
            { "KC_F5", "F5" },
            { "KC_F6", "F6" },
            { "KC_F7", "F7" },             
            { "KC_F8", "F8" },
            { "KC_F9", "F9" },
            { "KC_F10", "F10" },
            { "KC_F11", "F11" },
            { "KC_F12", "F12" },
            { "KC_F13", "F13" },
            { "KC_F14", "F14" },
            { "KC_F15", "F15" },
            { "KC_F16", "F16" },
            { "KC_F17", "F17" },
            { "KC_F18", "F18" },
            { "KC_F19", "F19" },
            { "KC_F20", "F20" },
            { "KC_F21", "F21" },             
            { "KC_F22", "F22" },
            { "KC_F23", "F23" },
            { "KC_F24", "F24" },
            
            // Keypad
            { "KC_P1", "Keypad\n1" },
            { "KC_P2", "Keypad\n2" },
            { "KC_P3", "Keypad\n3" },
            { "KC_P4", "Keypad\n4" },
            { "KC_P5", "Keypad\n5" },
            { "KC_P6", "Keypad\n6" },
            { "KC_P7", "Keypad\n7" },
            { "KC_P8", "Keypad\n8" },
            { "KC_P9", "Keypad\n9" },
            { "KC_P0", "Keypad\n0" },
            { "KC_PDOT", "Keypad\nDot" },
            { "KC_PCMM", "Keypad\nComma" },
            { "KC_PSLS", "Keypad\nSlash" },
            { "KC_PAST", "Keypad\nAsterisk" },
            { "KC_PMNS", "Keypad\nMinus" },
            { "KC_PPLS", "Keypad\nPlus" },
            { "KC_PEQL", "Keypad\nEqual" },
            { "KC_PENT", "Keypad\nEnter" },
            { "KC_NLCK", "NumLock" },
            
            // Mouse key
            { "KC_MS_U", "Mouse\nUp" },
            { "KC_MS_D", "Mouse\nDown" },
            { "KC_MS_L", "Mouse\nLeft" },
            { "KC_MS_R", "Mouse\nRight" },
            { "KC_BTN1", "Mouse\nBtn 1" },
            { "KC_BTN2", "Mouse\nBtn 2" },
            { "KC_BTN3", "Mouse\nBtn 3" },
            { "KC_BTN4", "Mouse\nBtn 4" },
            { "KC_BTN5", "Mouse\nBtn 5" },
            { "KC_WH_U", "Mouse\nWhl Up" },
            { "KC_WH_D", "Mouse\nWhl Down" },
            { "KC_WH_L", "Mouse\nWhl Left" },
            { "KC_WH_R", "Mouse\nWhl Right" },
            { "KC_ACL0", "Mouse\nACCEL0" },
            { "KC_ACL1", "Mouse\nACCEL1" },
            { "KC_ACL2", "Mouse\nACCEL2" },
            
            // System Control
            { "KC_PWR", "System\nPower" },
            { "KC_SLEP", "System\nSleep" },
            { "KC_WAKE", "System\nWake" },
            
            // Media/Audio Controls
            { "KC_MUTE", "Mute" },
            { "KC_VOLU", "Volume\nUp" },
            { "KC_VOLD", "Volume\nDown" },
            { "KC_MNXT", "Next\nTrack" },
            { "KC_MPRV", "Previous\nTrack" },
            { "KC_MFFD", "Fast\nForward" },
            { "KC_MRWD", "Rewind" },
            { "KC_MSTP", "Stop" },
            { "KC_MPLY", "Play\nPause" },
            { "KC_MSEL", "Select" },
            { "KC_EJCT", "Eject" },
            
            { "KC_MAIL", "Mail" },
            { "KC_CALC", "Calculator" },
            { "KC_MYCM", "My Computer" },
                      
            // Web    
            { "KC_WSCH", "WWW\nSearch" },
            { "KC_WHOM", "WWW\nHome" },
            { "KC_WBAK", "WWW\nBack" },
            { "KC_WFWD", "WWW\nForward" },
            { "KC_WSTP", "WWW\nStop" },
            { "KC_WREF", "WWW\nRefresh" },
            { "KC_WFAV", "WWW\nFavorites" },
            
            // Special
            { "KC_TRNS", "Trnsprnt" },
            { "BL_STEP", "Backlight" }
        };
    }
}
