﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Windows.Browser;
using System.IO;
using Silverlight;
using Ccflow.Web.UI.Control.Workflow.Designer;

namespace WF
{
    /// <summary>
    /// 窗口打开方式 
    /// </summary>
    public enum WindowModelEnum
    {
        Dialog,
        Window
    }
    public class Glo
    {
        #region 属性
        public static string BPMHost = null;
        #endregion

        #region 共用方法
        public static void OpenDialog(string url, string title, int h, int w)
        {
            OpenWindowOrDialog(url, title, string.Format("dialogHeight:{0}px;dialogWidth:{1}px", h, w), WindowModelEnum.Dialog);
        }

        public static void WinOpen(string url, string title, int h, int w)
        {
            OpenWindowOrDialog(url, title, string.Format("height={0},width={1}", h, w), WindowModelEnum.Window);
        }

        public static void OpenDialog(string url, string title)
        {
            OpenWindowOrDialog(url, title, "dialogHeight:600px;dialogWidth:800px", WindowModelEnum.Dialog);
        }

        /// <summary>
        /// 弹出网页窗口
        /// </summary>
        /// <param name="url">网页地址</param>
        private static void OpenWindowOrDialog(string url, string title, string property, WindowModelEnum windowModel)
        {
            if (WindowModelEnum.Dialog == windowModel)
            {
                HtmlPage.Window.Eval(
                    string.Format(
                        "window.showModalDialog('{0}',window,'dialogHeight:600px;dialogWidth:800px;help:no;scroll:auto;resizable:yes;status:no;');",
                        url));

            }
            else
            {
                HtmlPage.Window.Eval(
                    string.Format(
                        "window.open('{0}','{1}','{2};help=no,resizable=yes,status=no,scrollbars=1');", url,
                        title, property));
            }
        }
        #endregion 共用方法
    }
}
