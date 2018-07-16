using System;
using Microsoft.Win32;
using System.Configuration.Install;
using System.ComponentModel;
using System.Collections;
using System.IO;
using System.Windows.Forms;


namespace ClassForInstallation
{
    /// <summary>
    /// This CLass installs AddIn and BP rule for checking Composite Data Entity
    /// </summary>
    [RunInstaller(true)]
    public class ClassForInstallation : Installer
    {
        //Global variables which contains dll - files name and path to install
        RegistryKey rk;
        RegistryKey tmpRk;
        string registryPathAddIn = @"SOFTWARE\Microsoft\VisualStudio\14.0\ExtensionManager\EnabledExtensions";
        string addInsKey = "DynamicsRainierVSTools-52924ab1-c4fa-47fc-a90d-a5d1e69986bb";
        //string bpPartPath = @"\AOSService\PackagesLocalDirectory\Bin\BPExtensions";
        //string bpRuleFileName = "BPCompositeDataEntity.dll";
        string addInFileName = "D365O.Addin.AutoNewLabels.dll";
        string homeDirectory;
        public override void Install(IDictionary stateSaver)
        {
            MessageBox.Show("For correct installation, make sure that Visual Studio is closed", "CDEC Installer", MessageBoxButtons.OK);
            base.Install(stateSaver);
        }
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            homeDirectory = this.GetHomeDirectory(this.Context.Parameters.Values);
            this.MakeInstall(true);
        }
        /// <summary>
        /// This method finds directory where files were unpacked 
        /// </summary>
        /// <param name="contextParameters">context parameters of installer instance</param>
        /// <returns>path to installer library</returns>
        protected string GetHomeDirectory(ICollection contextParameters)
        {
            string homeDir = "";
            foreach (string home in contextParameters)
            {
                if (home.Contains("InstallerCDEC.dll"))
                {
                    homeDir = home.Replace("InstallerCDEC.dll", "");
                }
            }
            return homeDir.Length > 0 ? homeDir : null;
        }
        /// <summary>
        /// This method do actual job. Finds directory adn copy\delete fiels
        /// </summary>
        /// <param name="install"> true - install, false - delete</param>
        protected void MakeInstall(bool install)
        {
            if (Environment.Is64BitOperatingSystem)
            {
                rk = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.Users, RegistryView.Registry64);
                tmpRk = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.Users, RegistryView.Registry64);
            }
            else
            {
                rk = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.Users, RegistryView.Registry32);
                tmpRk = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.Users, RegistryView.Registry32);
            }
            // because Installer runs on NT AUTHORITY\SYSTEM user it is needed to look thru all users who have installed Visual Studio 2015 and D365FO
            foreach (string userName in rk.GetSubKeyNames())
            {
                string regPath = "";
                regPath = Path.Combine(userName, registryPathAddIn);
                tmpRk = rk.OpenSubKey(regPath);
                if (tmpRk != null)
                {
                    foreach (string nameVSExtension in tmpRk.GetValueNames())
                    {
                        if (nameVSExtension.Contains(addInsKey))
                        {
                            addInsKey = nameVSExtension;
                        }
                    }
                }
                if (tmpRk != null && install && tmpRk.GetValue(addInsKey) != null)
                {
                    File.Copy(Path.Combine(homeDirectory, addInFileName), Path.Combine(Path.Combine(tmpRk.GetValue(addInsKey).ToString(), "AddinExtensions"), addInFileName), true);
                    //File.Copy(Path.Combine(homeDirectory, bpRuleFileName), Path.Combine(Path.GetFullPath(bpPartPath), bpRuleFileName), true);
                }
                if (tmpRk != null && !install && tmpRk.GetValue(addInsKey) != null)
                {
                    File.Delete(Path.Combine(Path.Combine(tmpRk.GetValue(addInsKey).ToString(), "AddinExtensions"), addInFileName));
                    //File.Delete(Path.Combine(Path.GetFullPath(bpPartPath), bpRuleFileName));
                }

            }
        }
        public override void Uninstall(IDictionary savedState)
        {
            MessageBox.Show("For correct uninstall, make sure that Visual Studio is closed", "Uninstaller CDEC", MessageBoxButtons.OK);
            base.Uninstall(savedState);
            this.MakeInstall(false);

        }
    }
}
