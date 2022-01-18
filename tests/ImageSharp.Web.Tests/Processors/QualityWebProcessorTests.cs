// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Globalization;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Web.Commands;
using SixLabors.ImageSharp.Web.Commands.Converters;
using SixLabors.ImageSharp.Web.Processors;
using Xunit;

namespace SixLabors.ImageSharp.Web.Tests.Processors
{
    public class QualityWebProcessorTests
    {
        [Fact]
        public void QualityWebProcessor_UpdatesJpegQuality()
        {
            var parser = new CommandParser(new[] { new IntegralNumberConverter<int>() });
            CultureInfo culture = CultureInfo.InvariantCulture;

            var commands = new Dictionary<string, string>
            {
                { QualityWebProcessor.Quality, "42" },
            };

            using var image = new Image<Rgba32>(1, 1);
            using var formatted = new FormattedImage(image, JpegFormat.Instance);
            Assert.Equal(JpegFormat.Instance, formatted.Format);
            Assert.Equal(typeof(JpegEncoder), formatted.Encoder.GetType());

            new QualityWebProcessor()
                .Process(formatted, null, commands, parser, culture);

            Assert.Equal(JpegFormat.Instance, formatted.Format);
            Assert.Equal(42, ((JpegEncoder)formatted.Encoder).Quality);
        }

        [Fact]
        public void QualityWebProcessor_UpdatesWebpQuality()
        {
            var parser = new CommandParser(new[] { new IntegralNumberConverter<int>() });
            CultureInfo culture = CultureInfo.InvariantCulture;

            var commands = new Dictionary<string, string>
            {
                { QualityWebProcessor.Quality, "42" },
            };

            using var image = new Image<Rgba32>(1, 1);
            using var formatted = new FormattedImage(image, WebpFormat.Instance);
            Assert.Equal(WebpFormat.Instance, formatted.Format);
            Assert.Equal(typeof(WebpEncoder), formatted.Encoder.GetType());

            new QualityWebProcessor()
                .Process(formatted, null, commands, parser, culture);

            Assert.Equal(WebpFormat.Instance, formatted.Format);
            Assert.Equal(42, ((WebpEncoder)formatted.Encoder).Quality);
            Assert.Equal(WebpFileFormatType.Lossy, ((WebpEncoder)formatted.Encoder).FileFormat);
        }
    }
}