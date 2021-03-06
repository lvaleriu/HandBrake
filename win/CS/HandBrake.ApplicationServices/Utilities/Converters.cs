﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Converters.cs" company="HandBrake Project (http://handbrake.fr)">
//   This file is part of the HandBrake source code - It may be used under the terms of the GNU General Public License.
// </copyright>
// <summary>
//   A class to convert various things to native C# objects
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HandBrake.ApplicationServices.Utilities
{
    using System;

    using HandBrake.ApplicationServices.Interop.Model.Encoding;
    using HandBrake.ApplicationServices.Services.Encode.Model.Models;

    /// <summary>
    /// A class to convert various things to native C# objects
    /// </summary>
    public class Converters
    {
        /**
         * TODO:
         * - Many of these converters can be ditched at a later point. Should be able to model all this within the enums themsevles.
         * 
         **/

        #region Audio

        /// <summary>
        /// Get the GUI equiv to a CLI mixdown
        /// </summary>
        /// <param name="mixdown">
        /// The Audio Mixdown
        /// </param>
        /// <returns>
        /// The GUI representation of the mixdown
        /// </returns>
        [Obsolete("Use EnumHelper instead")]
        public static Mixdown GetAudioMixDown(string mixdown)
        {
            switch (mixdown.Trim())
            {
                case "Mono":
                    return Mixdown.Mono;
                case "Stereo":
                    return Mixdown.Stereo;
                case "Dolby Surround":
                    return Mixdown.DolbySurround;
                case "Dolby Pro Logic II":
                    return Mixdown.DolbyProLogicII;
                case "5.1 Channels":
                    return Mixdown.FivePoint1Channels;
                case "6.1 Channels":
                    return Mixdown.SixPoint1Channels;
                case "7.1 Channels":
                    return Mixdown.SevenPoint1Channels;
                case "7.1 (5F/2R/LFE)":
                    return Mixdown.Five_2_LFE;
                case "None":
                case "Passthru":
                    return Mixdown.None;
                default:
                    return Mixdown.Auto;
            }
        }

        /// <summary>
        /// Get the GUI equiv to a GUI audio encoder string
        /// </summary>
        /// <param name="audioEnc">
        /// The Audio Encoder
        /// </param>
        /// <returns>
        /// The GUI representation of that audio encoder
        /// </returns>
        [Obsolete("Use EnumHelper instead")]
        public static AudioEncoder GetAudioEncoder(string audioEnc)
        {
            switch (audioEnc)
            {
                case "AAC (faac)":         
                case "AAC (ffmpeg)":
                case "AAC (avcodec)":
                    return AudioEncoder.ffaac;
                case "AAC (FDK)":
                case "AAC (CoreAudio)":
                    return AudioEncoder.fdkaac;
                case "HE-AAC (FDK)":
                case "HE-AAC (CoreAudio)":
                case "HE-AAC":
                    return AudioEncoder.fdkheaac;
                case "MP3 (lame)":
                case "MP3":
                    return AudioEncoder.Lame;
                case "Vorbis (vorbis)":
                case "Vorbis":
                    return AudioEncoder.Vorbis;
                case "AC3 (ffmpeg)":
                case "AC3":
                    return AudioEncoder.Ac3;
                case "AC3 Passthru":
                    return AudioEncoder.Ac3Passthrough;
                case "DTS Passthru":
                    return AudioEncoder.DtsPassthrough;
                case "DTS-HD Passthru":
                    return AudioEncoder.DtsHDPassthrough;
                case "AAC Passthru":
                    return AudioEncoder.AacPassthru;
                case "MP3 Passthru":
                    return AudioEncoder.Mp3Passthru;
                case "FLAC (ffmpeg)":
                case "FLAC 16-bit":
                    return AudioEncoder.ffflac;
                case "FLAC (24-bit)":
                case "FLAC 24-bit":
                    return AudioEncoder.ffflac24;
                case "TrueHD Passthru":
                    return AudioEncoder.TrueHDPassthrough;
                case "E-AC3 Passthru":
                    return AudioEncoder.EAc3Passthrough;
                case "FLAC Passthru":
                    return AudioEncoder.FlacPassthru;
                case "Auto Passthru":
                    return AudioEncoder.Passthrough;
                default:
                    return AudioEncoder.ffaac;
            }
        }

        /// <summary>
        /// Get the CLI Audio Encoder name
        /// </summary>
        /// <param name="selectedEncoder">
        /// String The GUI Encode name
        /// </param>
        /// <returns>
        /// String CLI encoder name
        /// </returns>
        public static string GetCliAudioEncoder(AudioEncoder selectedEncoder)
        {
            return EnumHelper<AudioEncoder>.GetShortName(selectedEncoder);
        }

        #endregion

        #region Video

        /// <summary>
        /// Get the Video Encoder for a given string
        /// </summary>
        /// <param name="encoder">
        /// The encoder name
        /// </param>
        /// <returns>
        /// VideoEncoder enum object
        /// </returns>
        public static string GetVideoEncoder(VideoEncoder encoder)
        {
            switch (encoder)
            {
                case VideoEncoder.FFMpeg:
                    return "mpeg4";
                case VideoEncoder.FFMpeg2:
                    return "mpeg2";
                case VideoEncoder.X264:
                    return "x264";
                case VideoEncoder.QuickSync:
                    return "qsv_h264";
                case VideoEncoder.Theora:
                    return "theora";
                case VideoEncoder.X265:
                    return "x265";
                case VideoEncoder.VP8:
                    return "VP8";
                default:
                    return "x264";
            }
        }

        #endregion

        #region File Format

        /// <summary>
        /// Get the OutputFormat Enum for a given string
        /// </summary>
        /// <param name="format">
        /// OutputFormat as a string
        /// </param>
        /// <returns>
        /// An OutputFormat Enum
        /// </returns>
        public static OutputFormat GetFileFormat(string format)
        {
            switch (format.ToLower())
            {
                default:
                    return OutputFormat.Mp4;
                case "m4v":
                    return OutputFormat.Mp4;
                case "mkv":
                    return OutputFormat.Mkv;
            }
        }

        /// <summary>
        /// Get the OutputFormat Enum for a given string
        /// </summary>
        /// <param name="format">
        /// OutputFormat as a string
        /// </param>
        /// <returns>
        /// An OutputFormat Enum
        /// </returns>
        public static string GetFileFormat(OutputFormat format)
        {
            switch (format)
            {
                default:
                    return "mp4";
                case OutputFormat.Mp4:
                    return "m4v";
                case OutputFormat.Mkv:
                    return "mkv";
            }
        }

        #endregion
    }
}
