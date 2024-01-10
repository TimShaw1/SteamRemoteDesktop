﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class mainFrm : Form
    {
        static List<Screen> screens;
        public mainFrm()
        {
            screens = getScreenListSorted();
            InitializeComponent();
            Screen primaryScreen = Screen.PrimaryScreen;
            SetCursorPos(primaryScreen.Bounds.X + (int)(primaryScreen.Bounds.Width / 2), (int)(primaryScreen.Bounds.Height)/2);
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void hideBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void rightBtn_Click(object sender, EventArgs e)
        {
            var cursorPos = Cursor.Position;
            Screen currentScreen = Screen.FromPoint(cursorPos);
            int index = screens.FindIndex(screen => screen.Bounds.X==currentScreen.Bounds.X);
            try
            {
                SetCursorPos(screens[index + 1].Bounds.X + (int)(screens[index + 1].Bounds.Width / 2), cursorPos.Y);
                this.WindowState = FormWindowState.Normal;
                this.Location = screens[index + 1].Bounds.Location;
                this.WindowState = FormWindowState.Maximized;
            }
            catch
            {
                try
                {
                    SetCursorPos(screens[index - 1].Bounds.X + (int)(screens[index - 1].Bounds.Width / 2), cursorPos.Y);
                    this.WindowState = FormWindowState.Normal;
                    this.Location = this.Location = screens[index - 1].Bounds.Location;
                    this.WindowState = FormWindowState.Maximized;
                }
                catch { }
            }
        }

        private void leftBtn_Click(object sender, EventArgs e)
        {
            var cursorPos = Cursor.Position;
            Screen currentScreen = Screen.FromPoint(cursorPos);
            int index = screens.FindIndex(screen => screen.Bounds == currentScreen.Bounds);
            try
            {

                SetCursorPos(screens[index - 1].Bounds.X + (int)(screens[index - 1].Bounds.Width / 2), cursorPos.Y);
                this.WindowState = FormWindowState.Normal;
                this.Location = this.Location = screens[index - 1].Bounds.Location;
                this.WindowState = FormWindowState.Maximized;
            }
            catch
            {
                try
                {
                    SetCursorPos(screens[index + 1].Bounds.X + (int)(screens[index + 1].Bounds.Width / 2), cursorPos.Y);
                    this.WindowState = FormWindowState.Normal;
                    this.Location = screens[index + 1].Bounds.Location;
                    this.WindowState = FormWindowState.Maximized;
                }
                catch { }
            }

        }

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        private List<Screen> getScreenListSorted()
        {
            var list = new List<Screen>();
            foreach(var screen in Screen.AllScreens)
            {
                list.Add(screen);
            }

            var sortedList = list.OrderBy(screen=>screen.Bounds.X);
            return sortedList.ToList();
        }

    }
}
