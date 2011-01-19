using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml.Serialization;

namespace Tax666.Common
{
    public class DefaultConfigFileManager
    {
        #region 私有静态变量

        private static string m_configfilepath;                 // 文件所在路径变量
        private static IConfigInfo m_configinfo = null;         // 临时配置对象变量
        private static object m_lockHelper = new object();      // 锁对象

        #endregion

        #region 属性

        /// <summary>
        /// 文件所在路径
        /// </summary>
        public static string ConfigFilePath
        {
            get { return m_configfilepath; }
            set { m_configfilepath = value; }
        }

        /// <summary>
        /// 临时配置对象
        /// </summary>
        public static IConfigInfo ConfigInfo
        {
            get { return m_configinfo; }
            set { m_configinfo = value; }
        }

        #endregion

        /// <summary>
        /// 加载(反序列化)指定对象类型的配置对象
        /// </summary>
        /// <param name="fileoldchange">文件加载时间</param>
        /// <param name="configFilePath">配置文件所在路径</param>
        /// <param name="configinfo">相应的变量 注:该参数主要用于设置m_configinfo变量 和 获取类型.GetType()</param>
        /// <returns></returns>
        protected static IConfigInfo LoadConfig(ref DateTime fileoldchange, string configFilePath, IConfigInfo configinfo)
        {
            return LoadConfig(ref fileoldchange, configFilePath, configinfo, true);
        }

        /// <summary>
        /// 加载(反序列化)指定对象类型的配置对象
        /// </summary>
        /// <param name="fileoldchange">文件加载时间</param>
        /// <param name="configFilePath">配置文件所在路径(包括文件名)</param>
        /// <param name="configinfo">相应的变量 注:该参数主要用于设置m_configinfo变量 和 获取类型.GetType()</param>
        /// <param name="checkTime">是否检查并更新传递进来的"文件加载时间"变量</param>
        /// <returns></returns>
        protected static IConfigInfo LoadConfig(ref DateTime fileoldchange, string configFilePath, IConfigInfo configinfo, bool checkTime)
        {
            m_configfilepath = configFilePath;
            m_configinfo = configinfo;

            if (checkTime)
            {
                DateTime m_filenewchange = System.IO.File.GetLastWriteTime(configFilePath);

                //当程序运行中config文件发生变化时则对config重新赋值
                if (fileoldchange != m_filenewchange)
                {
                    fileoldchange = m_filenewchange;
                    lock (m_lockHelper)
                    {
                        m_configinfo = DeserializeInfo(configFilePath, configinfo.GetType());
                    }
                }
            }
            else
            {
                lock (m_lockHelper)
                {
                    m_configinfo = DeserializeInfo(configFilePath, configinfo.GetType());
                }

            }


            return m_configinfo;
        }

        /// <summary>
        /// 反序列化指定的类
        /// </summary>
        /// <param name="configfilepath">config 文件的路径</param>
        /// <param name="configtype">相应的类型</param>
        /// <returns></returns>
        public static IConfigInfo DeserializeInfo(string configfilepath, Type configtype)
        {

            IConfigInfo iconfiginfo = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(configfilepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(configtype);
                iconfiginfo = (IConfigInfo)serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                new Terminator().ThrowError("配置文件在序列化时发生错误。" + ex.Message);

            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }

            return iconfiginfo;
        }

        /// <summary>
        /// 保存配置实例(虚方法需继承)
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveConfig()
        {
            return true;
        }

        /// <summary>
        /// 保存(序列化)指定路径下的配置文件
        /// </summary>
        /// <param name="configFilePath">指定的配置文件所在的路径(包括文件名)</param>
        /// <param name="configinfo">被保存(序列化)的对象</param>
        /// <returns></returns>
        public bool SaveConfig(string configFilePath, IConfigInfo configinfo)
        {
            bool succeed = false;
            FileStream fs = null;
            try
            {
                fs = new FileStream(configFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(configinfo.GetType());
                serializer.Serialize(fs, configinfo);
                //成功则将会返回true
                succeed = true;
            }
            catch (Exception ex)
            {
                new Terminator().ThrowError("保存配置文件时发生错误。" + ex.Message);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }

            return succeed;
        }
    }
}
