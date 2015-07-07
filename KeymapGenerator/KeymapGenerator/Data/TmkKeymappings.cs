﻿using System.Collections.Generic;

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
            { "KC_PSCR", "Print Screen" },
            { "KC_SLCK", "Scroll Lock" },
            { "KC_PAUS", "Pause/Break" },
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
            { "KC_NUHS", "Non-US Hash" },
            { "KC_NUBS", "Non-US Backslash" },
            { "KC_LCAP", "Locking Caps" },
            { "KC_LNUM", "Locking Num" },
            { "KC_LSCR", "Locking Scroll" },
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
            { "KC_RIGHT", "Arrow Right" },
            { "KC_RGHT", "Arrow Right" },
            { "KC_LEFT", "Arrow Left" },           
            { "KC_DOWN", "Arrow Down" },
            { "KC_UP", "Arrow Up" },

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
            { "KC_P1", "Keypad 1" },
            { "KC_P2", "Keypad 2" },
            { "KC_P3", "Keypad 3" },
            { "KC_P4", "Keypad 4" },
            { "KC_P5", "Keypad 5" },
            { "KC_P6", "Keypad 6" },
            { "KC_P7", "Keypad 7" },
            { "KC_P8", "Keypad 8" },
            { "KC_P9", "Keypad 9" },
            { "KC_P0", "Keypad 0" },
            { "KC_PDOT", "Keypad Dot" },
            { "KC_PCMM", "Keypad Comma" },
            { "KC_PSLS", "Keypad Slash" },
            { "KC_PAST", "Keypad Asterisk" },
            { "KC_PMNS", "Keypad Minus" },
            { "KC_PPLS", "Keypad Plus" },
            { "KC_PEQL", "Keypad Equal" },
            { "KC_PENT", "Keypad Enter" },
            { "KC_NLCK", "NumLock" },
            
            // Mouse key
            { "KC_MS_U", "Mouse Up" },
            { "KC_MS_D", "Mouse Down" },
            { "KC_MS_L", "Mouse Left" },
            { "KC_MS_R", "Mouse Right" },
            { "KC_BTN1", "Mouse Button 1" },
            { "KC_BTN2", "Mouse Button 2" },
            { "KC_BTN3", "Mouse Button 3" },
            { "KC_BTN4", "Mouse Button 4" },
            { "KC_BTN5", "Mouse Button 5" },
            { "KC_WH_U", "Mouse Wheel Up" },
            { "KC_WH_D", "Mouse Wheel Down" },
            { "KC_WH_L", "Mouse Wheel Left" },
            { "KC_WH_R", "Mouse Wheel Right" },
            { "KC_ACL0", "Mouse ACCEL0" },
            { "KC_ACL1", "Mouse ACCEL1" },
            { "KC_ACL2", "Mouse ACCEL2" },
            
            // System Control
            { "KC_PWR", "System Power" },
            { "KC_SLEP", "System Sleep" },
            { "KC_WAKE", "System Wake" },
            
            // Media/Audio Controls
            { "KC_MUTE", "Mute" },
            { "KC_VOLU", "Volume Up" },
            { "KC_VOLD", "Volume Down" },
            { "KC_MNXT", "Next Track" },
            { "KC_MPRV", "Previous Track" },
            { "KC_MFFD", "Fast Forward" },
            { "KC_MRWD", "Rewind" },
            { "KC_MSTP", "Stop" },
            { "KC_MPLY", "Play/Pause" },
            { "KC_MSEL", "Select" },
            { "KC_EJCT", "Eject" },
            
            { "KC_MAIL", "Mail" },
            { "KC_CALC", "Calculator" },
            { "KC_MYCM", "My Computer" },
                      
            // Web    
            { "KC_WSCH", "WWW Search" },
            { "KC_WHOM", "WWW Home" },
            { "KC_WBAK", "WWW Back" },
            { "KC_WFWD", "WWW Forward" },
            { "KC_WSTP", "WWW Stop" },
            { "KC_WREF", "WWW Refresh" },
            { "KC_WFAV", "WWW Favorites" },
            
            // Special
            { "KC_TRNS", "Transparent" },
            { "BL_STEP", "Backlight" }
        };
    }
}
