using System;
using System.Collections.Generic;
using Spi;

namespace SPUtil
{
    class Opts
    {
        public string SiteURL;
        public string listname;
        public List<string> command;
        public bool verbose = false;

        private Opts()
        {
        }
        public static bool ParseOpts(string[] args, out Opts opts)
        {
            opts = null;
            bool showhelp = false;

            Opts tmpOpts = new Opts() { };
            var cmdOpts = new BeeOptsBuilder()
                .Add('s', "SiteURL",    OPTTYPE.VALUE, "Sharepoint site URL",           o => tmpOpts.SiteURL = o)
                .Add('l', "list",       OPTTYPE.VALUE, "name of the list to process",   o => tmpOpts.listname = o)
                .Add('h', "help",       OPTTYPE.BOOL, "show help", o => showhelp = true)
                .GetOpts();

            tmpOpts.command = BeeOpts.Parse(args, cmdOpts, (string unknownOpt) => Console.Error.WriteLine($"unknow option [{unknownOpt}]"));

            if ( String.IsNullOrEmpty(tmpOpts.SiteURL) )
            {
                showhelp = true;
                Console.Error.WriteLine("SiteURL is missing");
            }
            if (String.IsNullOrEmpty(tmpOpts.listname))
            {
                showhelp = true;
                Console.Error.WriteLine("name of the list is missing");
            }

            if (showhelp)
            {
                Console.WriteLine(
                      "\nusage: SPUtil.exe [OPTIONS]"
                    + "\n\nOptions:");
                BeeOpts.PrintOptions(cmdOpts);
                return false;
            }

            opts = tmpOpts;
            return true;
        }
    }
}