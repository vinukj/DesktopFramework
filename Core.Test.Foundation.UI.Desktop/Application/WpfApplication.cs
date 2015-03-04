


namespace Core.Test.Foundation.UI.Desktop
{
    using Microsoft.VisualStudio.TestTools.UITesting;
    using System.Diagnostics;

        /// <summary>
        /// Represent a WPF application
        /// </summary>
        public class WpfApplication
        {
            /// <summary>
            /// Store AUT instance
            /// </summary>
            private static ApplicationUnderTest aut;

            /// <summary>
            /// Start an application(AUT)
            /// </summary>
            /// <param name="exe">Exe path</param>
            public static void DesktopStart()
            {
                EnsurePlayBack();

                Close();
                //
               
                aut = ApplicationUnderTest.Launch(TestSession.Config.GetDesktopActiveEnvironment().UrlOrAppPath);

                AUTState.ApplicationHandle = aut.WindowHandle;

                AUTState.Application = aut.Process;
            }

            /// <summary>
            /// Attach to the existing instance
            /// </summary>
            public static void Attach()
            {
                Cleanup();

                EnsurePlayBack();

                aut = ApplicationUnderTest.FromProcess(AUTState.Application);

                AUTState.ApplicationHandle = aut.WindowHandle;

                AUTState.Application = aut.Process;
            }

            /// <summary>
            /// Clean the drivers
            /// </summary>
            public static void Cleanup()
            {
                if (Playback.IsInitialized)
                {
                    Playback.Cleanup();
                }
            }

            /// <summary>
            /// Close the application.
            /// </summary>
            public static void Close()
            {
                try
                {
                    //if (AUTState.Application != null)
                    //{
                        //AUTState.Application.Kill();
                        //AUTState.Application.WaitForExit();
                        //AUTState.Application.Dispose();
                        Process[] clients = Process.GetProcessesByName("Configuration Manager");
                        foreach (Process p in clients)
                        {
                            p.Kill();
                            p.WaitForExit();
                            p.Dispose();
                        }
                    //}
                }
                catch
                {
                }
            }

            /// <summary>
            /// Initialize playback
            /// </summary>
            private static void EnsurePlayBack()
            {
                if (!Playback.IsInitialized)
                {
                    Playback.Initialize();
                }
            }
        }

    }
