﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PDRenderer.cs" company="PerkinElmer Inc.">
//   Copyright (c) 2013 PerkinElmer Inc.,
//     940 Winter Street, Waltham, MA 02451.
//     All rights reserved.
//     This software is the confidential and proprietary information
//     of PerkinElmer Inc. ("Confidential Information"). You shall not
//     disclose such Confidential Information and may not use it in any way,
//     absent an express written license agreement between you and PerkinElmer Inc.
//     that authorizes such use.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
using Com.PerkinElmer.Service.PeptideSequenceRenderer.Models;
using Spotfire.Dxp.Application.Extension;
using Spotfire.Dxp.Application.Visuals.ValueRenderers;

#endregion

namespace Com.PerkinElmer.Service.PeptideSequenceRenderer.Renderer
{
    public sealed class PDRenderer : CustomValueRenderer
    {
        protected override void RenderCore(ValueRendererSettings rendererSettings, ValueRendererArgs rendererArgs, ValueRendererResult renderingResult)
        {
            int cellSize = rendererArgs.Height;

            PDRenderSettings settings = (PDRenderSettings) rendererSettings;

            Regex regex = new Regex(@"PEPTIDE1\{\[.*?_(.*?)\]\.(.*?)\}");

            var match = regex.Match(rendererArgs.DataValue.ValidValue.ToString());

            List<string> peptideList = new List<string>();

            peptideList.Add(match.Groups[1].Captures[0].Value);

            peptideList.AddRange(match.Groups[2].Captures[0].Value.Split(new char[] { '.' }).ToArray());

            Bitmap bitmap = new Bitmap(rendererArgs.Width, rendererArgs.Height);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                for (int i = 0; i < peptideList.Count; i++)
                {
                    string monomer = peptideList[i].Replace("[", string.Empty).Replace("]", string.Empty);

                    var color = PDRenderAddin.MonomerColorTable[monomer];

                    Rectangle rect = new Rectangle(i * cellSize, 0, cellSize, cellSize);

                    g.FillRectangle(new SolidBrush(ColorTranslator.FromHtml(color.BackgroundColor)), rect);
                    g.DrawRectangle(new Pen(Color.White), rect);

                    FontFamily fontFamily = new FontFamily("Tahoma");

                    fontFamily.GetEmHeight(FontStyle.Regular);
                    float ascent = fontFamily.GetCellAscent(FontStyle.Regular);
                    float descent = fontFamily.GetCellDescent(FontStyle.Regular);

                    float size = Convert.ToSingle(cellSize) / Convert.ToSingle(monomer.Length) * 0.5f * fontFamily.GetEmHeight(FontStyle.Regular) / (ascent + descent);

                    Font font = new Font(fontFamily, size, FontStyle.Regular);

                    StringFormat sf = new StringFormat();

                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    g.DrawString(monomer, font, new SolidBrush(ColorTranslator.FromHtml(color.ForeColor)), rect, sf);
                }

            }

            renderingResult.SetImage(bitmap);
            renderingResult.SetTooltip(rendererArgs.DataValue.ValidValue.ToString());

        }
    }
}
