﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Spotfire.Dxp.Application.Extension;
using Spotfire.Dxp.Framework.Preferences;
using Spotfire.Dxp.Framework.Persistence;
using Com.PerkinElmer.Service.PeptideSequenceRenderer.Properties;

namespace Com.PerkinElmer.Service.PeptideSequenceRenderer.Preference
{
    public enum Fonts
    {
        Arial,
        Tahoma,
        Courier
    }

    [PersistenceVersion(1, 0)]
    public class PDRenderPreference : CustomPreference
    {
        private readonly PreferenceProperty<int> _maxAminoAcids;
        private readonly PreferenceProperty<string> _colorCodingInformationLinkGuid;
        private readonly PreferenceProperty<string> _defaultFontColor;
        private readonly PreferenceProperty<string> _defaultBackgroundColor;
        private readonly PreferenceProperty<string> _branchMonomerFontColor;
        private readonly PreferenceProperty<string> _branchMonomerBackgroundColor;
        private readonly PreferenceProperty<Fonts> _font;

        public override string Category => Resources.AddinName;

        public override string SubCategory => "Renderer Settings";

        public PDRenderPreference()
        {
            _maxAminoAcids = AddPreference(new PreferenceProperty<int>(
                "Max number of amino acids", 
                "1.0", 
                PreferencePersistenceScope.Server, 
                PreferenceUsage.UserGroup,
                PDRenderAddin.DefaultFontSize));

            _colorCodingInformationLinkGuid = AddPreference(new PreferenceProperty<string>(
                "Color coding Information Link GUID", 
                "1.0",
                PreferencePersistenceScope.Server,
                PreferenceUsage.UserGroup));

            _defaultFontColor = AddPreference(new PreferenceProperty<string>(
                "Default font color (Hex)",
                "1.0",
                PreferencePersistenceScope.Server,
                PreferenceUsage.UserGroup));

            _defaultBackgroundColor = AddPreference(new PreferenceProperty<string>(
                "Default background color (Hex)",
                "1.0",
                PreferencePersistenceScope.Server,
                PreferenceUsage.UserGroup));

            _branchMonomerFontColor = AddPreference(new PreferenceProperty<string>(
                "Branch monomer font color (Hex)",
                "1.0",
                PreferencePersistenceScope.Server,
                PreferenceUsage.UserGroup));

            _branchMonomerBackgroundColor = AddPreference(new PreferenceProperty<string>(
                "Branch monomer background color (Hex)",
                "1.0",
                PreferencePersistenceScope.Server,
                PreferenceUsage.UserGroup));

            _font = AddPreference(new PreferenceProperty<Fonts>(
                "Font",
                "1.0",
                PreferencePersistenceScope.Server,
                PreferenceUsage.UserGroup,
                Fonts.Arial));
        }

        public int MaxAminoAcids
        {
            get { return _maxAminoAcids.Value; }
            set { _maxAminoAcids.Value = value; }
        }

        public string ColorCodingInformationLinkGuid
        {
            get { return _colorCodingInformationLinkGuid.Value; }
            set { _colorCodingInformationLinkGuid.Value = value; }
        }

        public string DefaultFontColor
        {
            get { return _defaultFontColor.Value; }
            set { _defaultFontColor.Value = value; }
        }

        public string DefaultBackgroundColor
        {
            get { return _defaultBackgroundColor.Value; }
            set { _defaultBackgroundColor.Value = value; }
        }

        public string BranchMonomerFontColor
        {
            get { return _branchMonomerFontColor.Value; }
            set { _branchMonomerFontColor.Value = value; }
        }

        public string BranchMonomerBackgroundColor
        {
            get { return _branchMonomerBackgroundColor.Value; }
            set { _branchMonomerBackgroundColor.Value = value; }
        }

        public Fonts Font
        {
            get { return _font.Value; }
            set { _font.Value = value; }
        }
    }
}