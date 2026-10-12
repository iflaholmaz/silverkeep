// xvv · Silverkeep Windows kurulumu
using System;
using System.IO;
using System.IO.Compression;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Reflection;

class Setup : Form {
 static string InstallDir=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Programs","AlbionDesk");
 static string DesktopLink=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),"Silverkeep.lnk");
 static string MenuLink=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu),"Programs","Silverkeep.lnk");
 const string Key="Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Silverkeep";
 Label status; ProgressBar progress; Button install; bool busy=false;
 [STAThread] static void Main(string[] args) {
  if(Array.IndexOf(args,"--remove-dir")>=0){for(int i=0;i<30;i++){try{if(Directory.Exists(InstallDir))Directory.Delete(InstallDir,true);break;}catch{System.Threading.Thread.Sleep(500);}}return;}
  Application.EnableVisualStyles();Application.SetCompatibleTextRenderingDefault(false);
  if(Array.IndexOf(args,"--uninstall")>=0){Uninstall();return;}
  if(Array.IndexOf(args,"--silent")>=0){File.WriteAllText(Path.Combine(Path.GetTempPath(),"silverkeep-install-result.txt"),"RUNNING");try{Install(null);File.WriteAllText(Path.Combine(Path.GetTempPath(),"silverkeep-install-result.txt"),"PASS");}catch(Exception e){File.WriteAllText(Path.Combine(Path.GetTempPath(),"silverkeep-install-result.txt"),"FAIL "+e);Environment.ExitCode=1;}return;}
  Application.Run(new Setup());
 }
 Setup(){Text="Silverkeep Kurulumu";ClientSize=new Size(520,340);FormBorderStyle=FormBorderStyle.FixedDialog;MaximizeBox=false;StartPosition=FormStartPosition.CenterScreen;BackColor=Color.FromArgb(247,248,242);Font=new Font("Segoe UI",10);try{Icon=new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("app.ico"));}catch{}
  var title=new Label{Text="Silverkeep",Font=new Font("Segoe UI",30,FontStyle.Bold),ForeColor=Color.FromArgb(36,76,60),Location=new Point(34,26),AutoSize=true};Controls.Add(title);
  var desc=new Label{Text="Albion Online için ekonomi masan.\nMarket, craft, gathering ve depolar tek yerde.",Location=new Point(38,99),Size=new Size(440,58)};Controls.Add(desc);
  status=new Label{Text="Kurulum masaüstüne bir Silverkeep kısayolu ekler.\nMevcut takip kayıtların korunur.",Location=new Point(38,178),Size=new Size(440,55),ForeColor=Color.FromArgb(110,124,111)};Controls.Add(status);
  progress=new ProgressBar{Location=new Point(38,237),Size=new Size(440,8),Visible=false};Controls.Add(progress);
  install=new Button{Text="Kur ve aç",Location=new Point(335,270),Size=new Size(143,42),BackColor=Color.FromArgb(36,76,60),ForeColor=Color.White,FlatStyle=FlatStyle.Flat};install.FlatAppearance.BorderSize=0;install.Click+=async(s,e)=>{if(busy)return;busy=true;install.Enabled=false;progress.Visible=true;status.Text="Silverkeep kuruluyor…";try{await Task.Run(()=>Install(n=>BeginInvoke(new Action(()=>{progress.Value=n;status.Text="Silverkeep kuruluyor… %"+n;}))));status.Text="Kurulum tamamlandı. Silverkeep açılıyor.";Process.Start(Path.Combine(InstallDir,"Silverkeep.exe"));busy=false;Close();}catch(Exception ex){busy=false;install.Enabled=true;status.Text="Kurulum tamamlanamadı.";MessageBox.Show(ex.Message,"Silverkeep",MessageBoxButtons.OK,MessageBoxIcon.Error);}};Controls.Add(install);FormClosing+=(s,e)=>{if(busy)e.Cancel=true;};
 }
 static void Install(Action<int> report){
  Directory.CreateDirectory(Path.GetDirectoryName(InstallDir));string stage=InstallDir+".kurulum-"+Guid.NewGuid().ToString("N"),backup=InstallDir+".onceki-"+Guid.NewGuid().ToString("N");
  foreach(var p in LegacyProcesses()){try{if(p.HasExited)continue;string exe=p.MainModule.FileName;if(String.Equals(exe,Path.Combine(InstallDir,"Silverkeep.exe"),StringComparison.OrdinalIgnoreCase)||String.Equals(exe,Path.Combine(InstallDir,"AlbionDesk.exe"),StringComparison.OrdinalIgnoreCase)){Process.Start(new ProcessStartInfo(exe,"--quit-for-install"){CreateNoWindow=true});if(!p.WaitForExit(5000))throw new Exception("Önce sistem tepsisinden Silverkeep’i tamamen kapat, ardından kurulumu tekrar başlat.");}}catch(System.ComponentModel.Win32Exception){}catch(System.InvalidOperationException){if(!p.HasExited)throw;}}
  bool moved=false;try{Directory.CreateDirectory(stage);using(var source=Assembly.GetExecutingAssembly().GetManifestResourceStream("payload.zip")){if(source==null)throw new Exception("Kurulum paketi bulunamadı.");using(var zip=new ZipArchive(source,ZipArchiveMode.Read)){int i=0;foreach(var entry in zip.Entries){string relative=entry.FullName.Replace('/',Path.DirectorySeparatorChar);if(!relative.StartsWith("Silverkeep"+Path.DirectorySeparatorChar,StringComparison.OrdinalIgnoreCase))continue;relative=relative.Substring("Silverkeep".Length+1);string dest=Path.GetFullPath(Path.Combine(stage,relative));if(!dest.StartsWith(Path.GetFullPath(stage)+Path.DirectorySeparatorChar,StringComparison.OrdinalIgnoreCase)&&dest!=stage)throw new Exception("Geçersiz paket yolu.");if(String.IsNullOrEmpty(entry.Name)){Directory.CreateDirectory(dest);}else{Directory.CreateDirectory(Path.GetDirectoryName(dest));entry.ExtractToFile(dest,true);}if(report!=null)report(Math.Min(90,++i*90/zip.Entries.Count));}}}
   if(!File.Exists(Path.Combine(stage,"Silverkeep.exe")))throw new Exception("Uygulama dosyası eksik.");
   using(var u=Assembly.GetExecutingAssembly().GetManifestResourceStream("uninstall.exe")){if(u==null)throw new Exception("Kaldırma aracı bulunamadı.");using(var f=File.Create(Path.Combine(stage,"Silverkeep-Kaldir.exe")))u.CopyTo(f);}
   if(Directory.Exists(InstallDir)){Directory.Move(InstallDir,backup);moved=true;}Directory.Move(stage,InstallDir);
   Shortcut(DesktopLink);Shortcut(MenuLink);string oldDesktop=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),"AlbionDesk.lnk"),oldMenu=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu),"Programs","AlbionDesk.lnk");if(File.Exists(oldDesktop))File.Delete(oldDesktop);if(File.Exists(oldMenu))File.Delete(oldMenu);Registry.CurrentUser.DeleteSubKeyTree("Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\AlbionDesk",false);
   using(var key=Registry.CurrentUser.CreateSubKey(Key)){key.SetValue("DisplayName","Silverkeep");key.SetValue("DisplayVersion","4.0.0");key.SetValue("Publisher","Silverkeep");key.SetValue("InstallLocation",InstallDir);key.SetValue("DisplayIcon",Path.Combine(InstallDir,"Silverkeep-31.ico"));key.SetValue("UninstallString","\""+Path.Combine(InstallDir,"Silverkeep-Kaldir.exe")+"\" --uninstall");key.SetValue("NoModify",1);key.SetValue("NoRepair",1);}
   if(moved)try{Directory.Delete(backup,true);}catch{}if(report!=null)report(100);
  }catch{if(Directory.Exists(stage))try{Directory.Delete(stage,true);}catch{}if(moved&&Directory.Exists(backup)){if(Directory.Exists(InstallDir))try{Directory.Delete(InstallDir,true);}catch{}if(!Directory.Exists(InstallDir))Directory.Move(backup,InstallDir);}throw;}
 }
 static Process[] LegacyProcesses(){var list=new System.Collections.Generic.List<Process>();list.AddRange(Process.GetProcessesByName("Silverkeep"));list.AddRange(Process.GetProcessesByName("AlbionDesk"));return list.ToArray();}
 static void Shortcut(string link){Type t=Type.GetTypeFromProgID("WScript.Shell");object shell=Activator.CreateInstance(t);object sc=t.InvokeMember("CreateShortcut",BindingFlags.InvokeMethod,null,shell,new object[]{link});Type c=sc.GetType();c.InvokeMember("TargetPath",BindingFlags.SetProperty,null,sc,new object[]{Path.Combine(InstallDir,"Silverkeep.exe")});c.InvokeMember("WorkingDirectory",BindingFlags.SetProperty,null,sc,new object[]{InstallDir});c.InvokeMember("IconLocation",BindingFlags.SetProperty,null,sc,new object[]{Path.Combine(InstallDir,"Silverkeep-31.ico")});c.InvokeMember("Save",BindingFlags.InvokeMethod,null,sc,null);}
 static void Uninstall(){if(MessageBox.Show("Silverkeep kaldırılsın mı?\nTakip kayıtların bilgisayarında korunacak.","Silverkeep",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes)return;try{foreach(var p in Process.GetProcessesByName("Silverkeep")){try{if(p.MainModule.FileName==Path.Combine(InstallDir,"Silverkeep.exe")){Process.Start(new ProcessStartInfo(p.MainModule.FileName,"--quit-for-install"){CreateNoWindow=true});if(!p.WaitForExit(5000))throw new Exception("Önce Silverkeep’i sistem tepsisinden tamamen kapat.");}}catch(System.ComponentModel.Win32Exception){}catch(System.InvalidOperationException){if(!p.HasExited)throw;}}if(File.Exists(DesktopLink))File.Delete(DesktopLink);if(File.Exists(MenuLink))File.Delete(MenuLink);Registry.CurrentUser.DeleteSubKeyTree(Key,false);using(var run=Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run",true)){if(run!=null){foreach(var name in run.GetValueNames()){string val=Convert.ToString(run.GetValue(name));if(val.IndexOf(Path.Combine(InstallDir,"Silverkeep.exe"),StringComparison.OrdinalIgnoreCase)>=0)run.DeleteValue(name,false);}}}string helper=Path.Combine(Path.GetTempPath(),"Silverkeep-Kaldir-"+Guid.NewGuid().ToString("N")+".exe");File.Copy(Application.ExecutablePath,helper);Process.Start(new ProcessStartInfo(helper,"--remove-dir"){CreateNoWindow=true});}catch(Exception e){MessageBox.Show(e.Message,"Silverkeep");}}
}





