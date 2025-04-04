using System;
using CoreGraphics;
using Foundation;
using Metal;
using ObjCRuntime;

#if NET
using Vector4 = global::System.Numerics.Vector4;
#else
using Vector4 = global::OpenTK.Vector4;
#endif

#if !NET
using NativeHandle = System.IntPtr;
#endif

namespace MetalPerformanceShaders {
	// MPSImageConvolution.h

	/// <summary>Filter that represents a convolution.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageConvolution_ClassReference/index.html">Apple documentation for <c>MPSImageConvolution</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageConvolution {
		[Export ("kernelHeight")]
		nuint KernelHeight { get; }

		[Export ("kernelWidth")]
		nuint KernelWidth { get; }

		[Export ("bias")]
		float Bias { get; set; }

		[Export ("initWithDevice:kernelWidth:kernelHeight:weights:")]
		[DesignatedInitializer]
		[Internal]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight, IntPtr kernelWeights);

		// inlining .ctor from base class

		[Export ("initWithDevice:")]
		NativeHandle Constructor (IMTLDevice device);

		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>An optimized Laplacian filter.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSImageLaplacian">Apple documentation for <c>MPSImageLaplacian</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageLaplacian {

		[Export ("bias")]
		float Bias { get; set; }

		// inlining .ctor from base class

		[Export ("initWithDevice:")]
		NativeHandle Constructor (IMTLDevice device);

		// inlining ctor from base class
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>Filter that blurs by transforming each pixel of the source image to the average of itself and its neighbors.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageBox_ClassReference/index.html">Apple documentation for <c>MPSImageBox</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageBox {
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("kernelHeight")]
		nuint KernelHeight { get; }

		[Export ("kernelWidth")]
		nuint KernelWidth { get; }

		[Export ("initWithDevice:kernelWidth:kernelHeight:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight);

		// [Export ("initWithDevice:")] marked as NS_UNAVAILABLE - You must use initWithDevice:kernelWidth:kernelHeight: instead.
	}

	/// <summary>Filter that blurs an image with a tent function.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageTent_ClassReference/index.html">Apple documentation for <c>MPSImageTent</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageBox))]
	[DisableDefaultCtor]
	interface MPSImageTent {

		// inlining .ctor from base class

		[Export ("initWithDevice:kernelWidth:kernelHeight:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight);
	}

	/// <summary>Filter that applies a fast Gaussian blur to an image.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageGaussianBlur_ClassReference/index.html">Apple documentation for <c>MPSImageGaussianBlur</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageGaussianBlur {
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("initWithDevice:sigma:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, float sigma);

		// [Export ("initWithDevice:")] marked as NS_UNAVAILABLE - You must use initWithDevice:sigma: instead.

		[Export ("sigma")]
		float Sigma { get; }
	}

	/// <summary>Filter that detects edges by using a Sobel filter.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageSobel_ClassReference/index.html">Apple documentation for <c>MPSImageSobel</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageSobel {
		// inlining .ctor from base class
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("initWithDevice:")]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithDevice:linearGrayColorTransform:")]
		[Internal]
		IntPtr InitWithDevice (IMTLDevice device, IntPtr /* float* */ transform);

		[Export ("colorTransform")]
		[Internal]
		IntPtr /* float* */ _ColorTransform { get; }
	}

	/// <summary>Base class for image pyramids.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSImagePyramid">Apple documentation for <c>MPSImagePyramid</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImagePyramid {
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("initWithDevice:")]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithDevice:centerWeight:")]
		NativeHandle Constructor (IMTLDevice device, float centerWeight);

		[Export ("initWithDevice:kernelWidth:kernelHeight:weights:")]
		[Internal]
		IntPtr InitWithDevice (IMTLDevice device, nuint kernelWidth, nuint kernelHeight, /* float* */ IntPtr kernelWeights);

		/// <summary>Gets the (odd) number of pixels in the filter window height.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("kernelHeight")]
		nuint KernelHeight { get; }

		/// <summary>Gets the (odd) number of pixels in the filter window width.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("kernelWidth")]
		nuint KernelWidth { get; }
	}

	/// <summary>Represents a Gaussian image pyramid.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSImageGaussianPyramid">Apple documentation for <c>MPSImageGaussianPyramid</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImagePyramid))]
	[DisableDefaultCtor]
	interface MPSImageGaussianPyramid {

		// inlining .ctor from base class

		[Export ("initWithDevice:kernelWidth:kernelHeight:weights:")]
		[Internal]
		IntPtr InitWithDevice (IMTLDevice device, nuint kernelWidth, nuint kernelHeight, /* float* */ IntPtr kernelWeights);

		// inlining ctor from base class
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	// MPSImageHistogram.h

	/// <summary>Calculates a histogram of image data.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageHistogram_ClassReference/index.html">Apple documentation for <c>MPSImageHistogram</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel))]
	[DisableDefaultCtor]
	interface MPSImageHistogram {
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("clipRectSource", ArgumentSemantic.Assign)]
		MTLRegion ClipRectSource { get; set; }

		[Export ("zeroHistogram")]
		bool ZeroHistogram { get; set; }

		[Export ("histogramInfo")]
		MPSImageHistogramInfo HistogramInfo {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			get;
		}

		// Could not initialize an instance of the type 'MetalPerformanceShaders.MPSImageHistogram': the native 'initWithDevice:' method returned nil.
		//		[Export ("initWithDevice:")]
		//		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithDevice:histogramInfo:")]
		[DesignatedInitializer]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		NativeHandle Constructor (IMTLDevice device, ref MPSImageHistogramInfo histogramInfo);

		[Export ("encodeToCommandBuffer:sourceTexture:histogram:histogramOffset:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, IMTLTexture source, IMTLBuffer histogram, nuint histogramOffset);

		[Export ("histogramSizeForSourceFormat:")]
		nuint GetHistogramSize (MTLPixelFormat sourceFormat);

		[MacCatalyst (13, 1)]
		[Export ("minPixelThresholdValue", ArgumentSemantic.Assign)]
		Vector4 MinPixelThresholdValue {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			get;
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			set;
		}
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSUnaryImageKernel" /> that equalizes the histogram of an image.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageHistogramEqualization_ClassReference/index.html">Apple documentation for <c>MPSImageHistogramEqualization</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageHistogramEqualization {
		[Export ("initWithDevice:histogramInfo:")]
		[DesignatedInitializer]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		NativeHandle Constructor (IMTLDevice device, ref MPSImageHistogramInfo histogramInfo);

		[Export ("histogramInfo")]
		MPSImageHistogramInfo HistogramInfo {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			get;
		}

		[Export ("encodeTransformToCommandBuffer:sourceTexture:histogram:histogramOffset:")]
		void EncodeTransformToCommandBuffer (IMTLCommandBuffer commandBuffer, IMTLTexture source, IMTLBuffer histogram, nuint histogramOffset);

		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>Transforms an image so that its histogram matches a desired histogram.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageHistogramSpecification_ClassReference/index.html">Apple documentation for <c>MPSImageHistogramSpecification</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageHistogramSpecification {
		[Export ("initWithDevice:histogramInfo:")]
		[DesignatedInitializer]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		NativeHandle Constructor (IMTLDevice device, ref MPSImageHistogramInfo histogramInfo);

		[Export ("histogramInfo")]
		MPSImageHistogramInfo HistogramInfo {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			get;
		}

		[Export ("encodeTransformToCommandBuffer:sourceTexture:sourceHistogram:sourceHistogramOffset:desiredHistogram:desiredHistogramOffset:")]
		void EncodeTransformToCommandBuffer (IMTLCommandBuffer commandBuffer, IMTLTexture source, IMTLBuffer sourceHistogram, nuint sourceHistogramOffset, IMTLBuffer desiredHistogram, nuint desiredHistogramOffset);

		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	// MPSImageIntegral.h

	/// <summary>Filter that sums the values of pixels in a region.</summary>
	///     <remarks>
	///       <para>The region of consideration is given by the filter offset and the position in the result image. That is, each pixel of the destination image is the sum of the pixels in a rectangular region bounded by the coordinates of the target pixel and the filter offset.</para>
	///     </remarks>
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageIntegral_ClassReference/index.html">Apple documentation for <c>MPSImageIntegral</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageIntegral {
		// inlining .ctor from base class

		[Export ("initWithDevice:")]
		NativeHandle Constructor (IMTLDevice device);

		// inlining ctor from base class
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>Filter that sums the squared values of pixels in a region.</summary>
	///     <remarks>
	///       <para>The region of consideration is given by the filter offset and the position in the result image. That is, each pixel of the destination image is the sum of the pixels in a rectangular region bounded by the coordinates of the target pixel and the filter offset.</para>
	///     </remarks>
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageIntegralOfSquares_ClassReference/index.html">Apple documentation for <c>MPSImageIntegralOfSquares</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageIntegralOfSquares {
		// inlining .ctor from base class

		[Export ("initWithDevice:")]
		NativeHandle Constructor (IMTLDevice device);

		// inlining ctor from base class
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	// MPSImageKernel.h

	/// <summary>Represents a shader transformation produces one texture from another.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSUnaryImageKernel_ClassReference/index.html">Apple documentation for <c>MPSUnaryImageKernel</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel))]
	[DisableDefaultCtor]
	interface MPSUnaryImageKernel {
		[Export ("offset", ArgumentSemantic.Assign)]
		MPSOffset Offset { get; set; }

		[Export ("clipRect", ArgumentSemantic.Assign)]
		MTLRegion ClipRect { get; set; }

		[Export ("edgeMode", ArgumentSemantic.Assign)]
		MPSImageEdgeMode EdgeMode { get; set; }

		[Export ("encodeToCommandBuffer:inPlaceTexture:fallbackCopyAllocator:")]
		bool EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, out NSObject /* IMTLTexture */ texture, [NullAllowed] MPSCopyAllocator copyAllocator);
		// FIXME: can't use IMTLTexture now

		[Export ("encodeToCommandBuffer:sourceTexture:destinationTexture:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, IMTLTexture sourceTexture, IMTLTexture destinationTexture);

		[MacCatalyst (13, 1)]
		[Export ("encodeToCommandBuffer:sourceImage:destinationImage:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSImage sourceImage, MPSImage destinationImage);

		[Export ("sourceRegionForDestinationSize:")]
		MPSRegion SourceRegionForDestinationSize (MTLSize destinationSize);

		// inlining .ctor from base class

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>A image kernel that combines two textures into one texture result.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSBinaryImageKernel_ClassReference/index.html">Apple documentation for <c>MPSBinaryImageKernel</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel))]
	[DisableDefaultCtor]
	interface MPSBinaryImageKernel {
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		/// <summary>Gets or sets the location of the destination clipping rectangle in the primary texture.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("primaryOffset", ArgumentSemantic.Assign)]
		MPSOffset PrimaryOffset { get; set; }

		/// <summary>Gets or sets the location of the destination clipping rectangle in the secondary texture.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("secondaryOffset", ArgumentSemantic.Assign)]
		MPSOffset SecondaryOffset { get; set; }

		/// <summary>Gets or sets the behavior to use when the shader encounters the edge of the primary image.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("primaryEdgeMode", ArgumentSemantic.Assign)]
		MPSImageEdgeMode PrimaryEdgeMode { get; set; }

		/// <summary>Gets or sets the behavior to use when the shader encounters the edge of the secondary image.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("secondaryEdgeMode", ArgumentSemantic.Assign)]
		MPSImageEdgeMode SecondaryEdgeMode { get; set; }

		/// <summary>The region of the destination in which to write data.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("clipRect", ArgumentSemantic.Assign)]
		MTLRegion ClipRect { get; set; }

		[Export ("encodeToCommandBuffer:primaryTexture:inPlaceSecondaryTexture:fallbackCopyAllocator:")]
		bool EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, IMTLTexture primaryTexture, out NSObject /* IMTLTexture */ inPlaceSecondaryTexture, [NullAllowed] MPSCopyAllocator copyAllocator);
		// FIXME: can't use IMTLTexture now

		[Export ("encodeToCommandBuffer:inPlacePrimaryTexture:secondaryTexture:fallbackCopyAllocator:")]
		bool EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, out NSObject /* MTLTexture */ inPlacePrimaryTexture, IMTLTexture secondaryTexture, [NullAllowed] MPSCopyAllocator copyAllocator);
		// FIXME: can't use IMTLTexture now

		[Export ("encodeToCommandBuffer:primaryTexture:secondaryTexture:destinationTexture:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, IMTLTexture primaryTexture, IMTLTexture secondaryTexture, IMTLTexture destinationTexture);

		[MacCatalyst (13, 1)]
		[Export ("encodeToCommandBuffer:primaryImage:secondaryImage:destinationImage:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSImage primaryImage, MPSImage secondaryImage, MPSImage destinationImage);

		[Export ("primarySourceRegionForDestinationSize:")]
		MPSRegion PrimarySourceRegionForDestinationSize (MTLSize destinationSize);

		[Export ("secondarySourceRegionForDestinationSize:")]
		MPSRegion SecondarySourceRegionForDestinationSize (MTLSize destinationSize);

		// inlining .ctor from base class

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	// MPSImageMedian.h

	/// <summary>Filter that finds the median value of each channel for pixels in the region around each source image pixel.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageMedian_ClassReference/index.html">Apple documentation for <c>MPSImageMedian</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageMedian {
		[Export ("kernelDiameter")]
		nuint KernelDiameter { get; }

		[Export ("initWithDevice:kernelDiameter:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelDiameter);

		// [Export ("initWithDevice:")] is NS_UNAVAILABLE - You must use initWithDevice:kernelDiameter: instead.

		[Static]
		[Export ("maxKernelDiameter")]
		nuint MaxKernelDiameter { get; }

		[Static]
		[Export ("minKernelDiameter")]
		nuint MinKernelDiameter { get; }

		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	// MPSImageMorphology.h

	/// <summary>Filter that finds the maximum pixel value in a window around each pixel in the source image.(Individual channels are processed separately.)</summary>
	///     <remarks>
	///       <para>Pixels inside the window that is considered but that are outside the bounds of the source image are clamped to the value at the edge of the image.</para>
	///     </remarks>
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageAreaMax_ClassReference/index.html">Apple documentation for <c>MPSImageAreaMax</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageAreaMax {
		[Export ("kernelHeight")]
		nuint KernelHeight { get; }

		[Export ("kernelWidth")]
		nuint KernelWidth { get; }

		[Export ("initWithDevice:kernelWidth:kernelHeight:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight);

		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		// [Export ("initWithDevice:")] is NS_UNAVAILABLE - You must use initWithDevice:kernelWidth:kernelHeight: instead.
	}

	/// <summary>Filter that finds the minimum pixel value in a window around each pixel in the source image.(Individual channels are processed separately.)</summary>
	///     <remarks>
	///       <para>Pixels inside the window that is considered but that are outside the bounds of the source image are clamped to the value at the edge of the image.</para>
	///       <para>Developers should note that this filter kernel inherits from <see cref="T:MetalPerformanceShaders.MPSImageAreaMax" />.</para>
	///     </remarks>
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageAreaMin_ClassReference/index.html">Apple documentation for <c>MPSImageAreaMin</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageAreaMax))]
	[DisableDefaultCtor]
	interface MPSImageAreaMin {
		// inlining ctor from base class

		[Export ("initWithDevice:kernelWidth:kernelHeight:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight);
	}

	/// <summary>Finds the maximum value in a region, offset by a corresponding value in a mask.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageDilate_ClassReference/index.html">Apple documentation for <c>MPSImageDilate</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageDilate {
		/// <summary>Gets the height of the window around the pixel to consider. This is always an odd number.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("kernelHeight")]
		nuint KernelHeight { get; }

		/// <summary>Gets the height of the window around the pixel to consider. This is always an odd number.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("kernelWidth")]
		nuint KernelWidth { get; }

		[Export ("initWithDevice:kernelWidth:kernelHeight:values:")]
		[Internal]
		IntPtr InitWithDevice (IMTLDevice device, nuint kernelWidth, nuint kernelHeight, IntPtr values);

		// [Export ("initWithDevice:")] is NS_UNAVAILABLE - You must use initWithDevice:kernelWidth:kernelHeight:values: instead. instead.

		// inlining ctor from base class
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>Finds the mininum value in a region, offset by a corresponding value in a mask.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageErode_ClassReference/index.html">Apple documentation for <c>MPSImageErode</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageDilate))]
	[DisableDefaultCtor]
	interface MPSImageErode {

		// inlining ctor from base class -> done in manual bindings (wrt float* argument)

		// inlining ctor from base class
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	// MPSImageResampling.h

	/// <summary>Filter that performs a scaling operation with Lanczos resampling.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageLanczosScale_ClassReference/index.html">Apple documentation for <c>MPSImageLanczosScale</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageScale))]
	[DisableDefaultCtor]
	interface MPSImageLanczosScale {
		// inlining .ctor from base class

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		// inlining ctor from base class
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	// MPSImageThreshold.h

	/// <summary>Filter that changes all pixels above a threshold luminance to a specified maximum single-channel value, and completely darkens the rest.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageThresholdBinary_ClassReference/index.html">Apple documentation for <c>MPSImageThresholdBinary</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageThresholdBinary {
		[Export ("initWithDevice:thresholdValue:maximumValue:linearGrayColorTransform:")]
		[Internal]
		IntPtr InitWithDevice (IMTLDevice device, float thresholdValue, float maximumValue, /* [NullAllowed] float* */ IntPtr transform);

		// [Export ("initWithDevice:")] marked as NS_UNAVAILABLE - You must use initWithDevice:thresholdValue:maximumValue:linearGrayColorTransform: instead

		/// <summary>Gets the value above which pixels will be brightened to the maximum value.</summary>
		///         <value>The value above which pixels will be brightened to the maximum value.</value>
		///         <remarks>To be added.</remarks>
		[Export ("thresholdValue")]
		float ThresholdValue { get; }

		/// <summary>Gets the value to which to brighten pixels brighter than the threshold.</summary>
		///         <value>The value to which to brighten pixels brighter than the threshold.</value>
		///         <remarks>To be added.</remarks>
		[Export ("maximumValue")]
		float MaximumValue { get; }

		[Export ("transform")]
		[Internal]
		IntPtr _Transform { get; }

		// inlining ctor from base class
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>Filter that changes all pixels above a threshold luminance to 0, and brightens the rest to a specified maximum single-channel value.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageThresholdBinaryInverse_ClassReference/index.html">Apple documentation for <c>MPSImageThresholdBinaryInverse</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageThresholdBinaryInverse {
		[Export ("initWithDevice:thresholdValue:maximumValue:linearGrayColorTransform:")]
		[Internal]
		IntPtr InitWithDevice (IMTLDevice device, float thresholdValue, float maximumValue, /* [NullAllowed] float* */ IntPtr transform);

		// [Export ("initWithDevice:")] marked as NS_UNAVAILABLE - You must use initWithDevice:thresholdValue:maximumValue:linearGrayColorTransform: instead

		/// <summary>Gets the value above which pixels will be darkened to 0 brightness.</summary>
		///         <value>The value above which pixels will be darkened to 0 brightness.</value>
		///         <remarks>To be added.</remarks>
		[Export ("thresholdValue")]
		float ThresholdValue { get; }

		/// <summary>Gets the value to which to brighten pixels that are dimmer than the threshold in the source image.</summary>
		///         <value>The value to which to brighten pixels that are dimmer than the threshold in the source image.</value>
		///         <remarks>To be added.</remarks>
		[Export ("maximumValue")]
		float MaximumValue { get; }

		[Export ("transform")]
		[Internal]
		IntPtr _Transform { get; }

		// inlining ctor from base class
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>Filter that clamps brightness values to a threshold value.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageThresholdTruncate_ClassReference/index.html">Apple documentation for <c>MPSImageThresholdTruncate</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageThresholdTruncate {
		[Export ("initWithDevice:thresholdValue:linearGrayColorTransform:")]
		[Internal]
		IntPtr InitWithDevice (IMTLDevice device, float thresholdValue, /* [NullAllowed] float* */ IntPtr transform);

		// [Export ("initWithDevice:")] marked as NS_UNAVAILABLE - You must use initWithDevice:thresholdValue:linearGrayColorTransform: instead

		/// <summary>Gets the value to which pixel brightensses will be clamped.</summary>
		///         <value>The value to which pixel brightensses will be clamped.</value>
		///         <remarks>To be added.</remarks>
		[Export ("thresholdValue")]
		float ThresholdValue { get; }

		[Export ("transform")]
		[Internal]
		IntPtr _Transform { get; }

		// inlining ctor from base class
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>Filter that darkens all pixels dimmer than or equal in brightness to a threshold luminance to 0, and leaves the rest unchanged.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageThresholdToZero_ClassReference/index.html">Apple documentation for <c>MPSImageThresholdToZero</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageThresholdToZero {
		[Export ("initWithDevice:thresholdValue:linearGrayColorTransform:")]
		[Internal]
		IntPtr InitWithDevice (IMTLDevice device, float thresholdValue, /* [NullAllowed] float* */ IntPtr transform);

		// [Export ("initWithDevice:")] marked as NS_UNAVAILABLE - You must use initWithDevice:thresholdValue:linearGrayColorTransform: instead

		/// <summary>Gets the value above which pixels will be left unchanged.</summary>
		///         <value>The value above which pixels will be left unchanged.</value>
		///         <remarks>To be added.</remarks>
		[Export ("thresholdValue")]
		float ThresholdValue { get; }

		[Export ("transform")]
		[Internal]
		IntPtr _Transform { get; }

		// inlining ctor from base class
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>Filter that leaves all pixels dimmer than or equal in brightness to a threshold luminance unchangedt, and darkens the rest to 0.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageThresholdToZeroInverse_ClassReference/index.html">Apple documentation for <c>MPSImageThresholdToZeroInverse</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageThresholdToZeroInverse {
		[Export ("initWithDevice:thresholdValue:linearGrayColorTransform:")]
		[Internal]
		IntPtr InitWithDevice (IMTLDevice device, float thresholdValue, /* [NullAllowed] float* */ IntPtr transform);

		// [Export ("initWithDevice:")] marked as NS_UNAVAILABLE - You must use initWithDevice:thresholdValue:linearGrayColorTransform: instead

		/// <summary>Gets the value above which pixels will be darkened to 0.</summary>
		///         <value>The value above which pixels will be darkened to 0.</value>
		///         <remarks>To be added.</remarks>
		[Export ("thresholdValue")]
		float ThresholdValue { get; }

		[Export ("transform")]
		[Internal]
		IntPtr _Transform { get; }

		// inlining ctor from base class
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	// MPSKernel.h

	/// <include file="../docs/api/MetalPerformanceShaders/MPSKernel.xml" path="/Documentation/Docs[@DocId='T:MetalPerformanceShaders.MPSKernel']/*" />
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MPSKernel : NSCopying, NSSecureCoding {
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		/// <summary>Gets or sets the list of kernel options for running the shader.</summary>
		///         <value>The list of kernel options for running the shader.</value>
		///         <remarks>
		///           <para>Application developers can use this field to allow reduced-precision types in calculations, skip validation, both, or neither.</para>
		///         </remarks>
		[Export ("options", ArgumentSemantic.Assign)]
		MPSKernelOptions Options { get; set; }

		/// <summary>Gets the device for which the kernel will be encoded.</summary>
		///         <value>The device for which the kernel will be encoded.</value>
		///         <remarks>To be added.</remarks>
		[Export ("device", ArgumentSemantic.Retain)]
		IMTLDevice Device { get; }

		/// <summary>Gets or sets a programmer-friendly name for the shader</summary>
		///         <value>
		///           <para>(More documentation for this node is coming)</para>
		///           <para tool="nullallowed">This value can be <see langword="null" />.</para>
		///         </value>
		///         <remarks>To be added.</remarks>
		[NullAllowed, Export ("label")]
		string Label { get; set; }

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("copyWithZone:device:")]
		[return: Release ()]
		MPSKernel CopyWithZone ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);
	}

	// MPSImageTranspose.h

	/// <summary>Filter that transposes an image.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/library/ios/documentation/MetalPerformanceShaders/Reference/MPSImageTranspose_ClassReference/index.html">Apple documentation for <c>MPSImageTranspose</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageTranspose {

		// inlining .ctor from base class

		[Export ("initWithDevice:")]
		NativeHandle Constructor (IMTLDevice device);

		// inlining ctor from base class
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	// MPSCNN.h

	/// <summary>The base class for layers in a convolutional neural network.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSCNNKernel">Apple documentation for <c>MPSCNNKernel</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel), Name = "MPSCNNKernel")]
	[Abstract]
	[DisableDefaultCtor]
	interface MPSCnnKernel {
		// inlining .ctor from base class

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("offset", ArgumentSemantic.Assign)]
		MPSOffset Offset { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("clipRect", ArgumentSemantic.Assign)]
		MTLRegion ClipRect { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("destinationFeatureChannelOffset")]
		nuint DestinationFeatureChannelOffset { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("sourceFeatureChannelMaxCount")]
		nuint SourceFeatureChannelMaxCount { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("sourceFeatureChannelOffset")]
		nuint SourceFeatureChannelOffset { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("edgeMode", ArgumentSemantic.Assign)]
		MPSImageEdgeMode EdgeMode { get; set; }

		[Export ("encodeToCommandBuffer:sourceImage:destinationImage:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSImage sourceImage, MPSImage destinationImage);

		[MacCatalyst (13, 1)]
		[Export ("encodeToCommandBuffer:sourceImage:destinationState:destinationImage:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSImage sourceImage, MPSState destinationState, MPSImage destinationImage);

		[MacCatalyst (13, 1)]
		[Export ("encodeBatchToCommandBuffer:sourceImages:destinationImages:")]
		void EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceImages, NSArray<MPSImage> destinationImages);

		[MacCatalyst (13, 1)]
		[Export ("encodeBatchToCommandBuffer:sourceImages:destinationStates:destinationImages:")]
		void EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceImages, [NullAllowed] NSArray<MPSState> destinationStates, NSArray<MPSImage> destinationImages);

		[Export ("sourceRegionForDestinationSize:")]
		MPSRegion GetSourceRegion (MTLSize destinationSize);

		//inlining ctor from base class
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("isBackwards")]
		bool IsBackwards { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("isStateModified")]
		bool IsStateModified { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("padding", ArgumentSemantic.Retain)]
		IMPSNNPadding Padding { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("destinationImageAllocator", ArgumentSemantic.Retain)]
		IMPSImageAllocator DestinationImageAllocator { get; set; }

		[MacCatalyst (13, 1)]
		[Export ("encodeToCommandBuffer:sourceImage:")]
		MPSImage EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSImage sourceImage);

		[MacCatalyst (13, 1)]
		[Export ("encodeToCommandBuffer:sourceImage:destinationState:destinationStateIsTemporary:")]
		MPSImage EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSImage sourceImage, [NullAllowed] out MPSState outState, bool isTemporary);

		[MacCatalyst (13, 1)]
		[Export ("encodeBatchToCommandBuffer:sourceImages:")]
		NSArray<MPSImage> EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceImages);

		[MacCatalyst (13, 1)]
		[Export ("encodeBatchToCommandBuffer:sourceImages:destinationStates:destinationStateIsTemporary:")]
		NSArray<MPSImage> EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceImages, [NullAllowed] out NSArray<MPSState> outStates, bool isTemporary);

		[MacCatalyst (13, 1)]
		[Export ("resultStateForSourceImage:sourceStates:destinationImage:")]
		[return: NullAllowed]
		MPSState GetResultState (MPSImage sourceImage, [NullAllowed] NSArray<MPSState> sourceStates, MPSImage destinationImage);

		[MacCatalyst (13, 1)]
		[Export ("resultStateBatchForSourceImage:sourceStates:destinationImage:")]
		[return: NullAllowed]
		NSArray<MPSState> GetResultStateBatch (NSArray<MPSImage> sourceImage, [NullAllowed] NSArray<MPSState> [] sourceStates, NSArray<MPSImage> destinationImage);

		[MacCatalyst (13, 1)]
		[Export ("temporaryResultStateForCommandBuffer:sourceImage:sourceStates:destinationImage:")]
		[return: NullAllowed]
		MPSState GetTemporaryResultState (IMTLCommandBuffer commandBuffer, MPSImage sourceImage, [NullAllowed] NSArray<MPSState> sourceStates, MPSImage destinationImage);

		[MacCatalyst (13, 1)]
		[Export ("temporaryResultStateBatchForCommandBuffer:sourceImage:sourceStates:destinationImage:")]
		[return: NullAllowed]
		NSArray<MPSState> GetTemporaryResultStateBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceImage, [NullAllowed] NSArray<MPSState> [] sourceStates, NSArray<MPSImage> destinationImage);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("isResultStateReusedAcrossBatch")]
		bool IsResultStateReusedAcrossBatch { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("appendBatchBarrier")]
		bool AppendBatchBarrier { get; }

		[MacCatalyst (13, 1)]
		[Export ("destinationImageDescriptorForSourceImages:sourceStates:")]
		MPSImageDescriptor GetDestinationImageDescriptor (NSArray<MPSImage> sourceImages, [NullAllowed] NSArray<MPSState> sourceStates);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("kernelWidth")]
		nuint KernelWidth { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("kernelHeight")]
		nuint KernelHeight { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("strideInPixelsX")]
		nuint StrideInPixelsX { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("strideInPixelsY")]
		nuint StrideInPixelsY { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("dilationRateX")]
		nuint DilationRateX { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("dilationRateY")]
		nuint DilationRateY { get; }
	}

	/// <summary>The base class for activation functions.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSCNNNeuron">Apple documentation for <c>MPSCNNNeuron</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel), Name = "MPSCNNNeuron")]
	[Abstract] // You must use one of the sub-classes of MPSCNNNeuron
	[DisableDefaultCtor]
	interface MPSCnnNeuron {
		// inlining .ctor from base class

		[Export ("initWithDevice:")]
		NativeHandle Constructor (IMTLDevice device);

		// inlining ctor from base class
		[MacCatalyst (13, 1)]
		[DesignatedInitializer]
		[Export ("initWithCoder:device:")]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("neuronType")]
		MPSCnnNeuronType NeuronType { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("a")]
		float A { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("b")]
		float B { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("c")]
		float C { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("data", ArgumentSemantic.Retain)]
		NSData Data { get; }

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:neuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSNNNeuronDescriptor neuronDescriptor);
	}

	/// <summary>The linear activation function.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSCNNNeuronLinear">Apple documentation for <c>MPSCNNNeuronLinear</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuron), Name = "MPSCNNNeuronLinear")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronLinear {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("a")]
		float A { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("b")]
		float B { get; }

		[Deprecated (PlatformName.TvOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Export ("initWithDevice:a:b:")]
		NativeHandle Constructor (IMTLDevice device, float a, float b);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:neuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSNNNeuronDescriptor neuronDescriptor);
	}

	/// <summary>The rectified linear unit activation function.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSCNNNeuronReLU">Apple documentation for <c>MPSCNNNeuronReLU</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuron), Name = "MPSCNNNeuronReLU")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronReLU {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("a")]
		float A { get; }

		[Deprecated (PlatformName.TvOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Export ("initWithDevice:a:")]
		NativeHandle Constructor (IMTLDevice device, float a);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:neuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSNNNeuronDescriptor neuronDescriptor);
	}

	/// <summary>The sigmoid activation function.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSCNNNeuronSigmoid">Apple documentation for <c>MPSCNNNeuronSigmoid</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuron), Name = "MPSCNNNeuronSigmoid")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronSigmoid {

		[Deprecated (PlatformName.TvOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Export ("initWithDevice:")]
		NativeHandle Constructor (IMTLDevice device);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:neuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSNNNeuronDescriptor neuronDescriptor);
	}

	/// <summary>The hyperbolic tangent activation function.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSCNNNeuronTanH">Apple documentation for <c>MPSCNNNeuronTanH</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuron), Name = "MPSCNNNeuronTanH")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronTanH {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("a")]
		float A { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("b")]
		float B { get; }

		[Deprecated (PlatformName.TvOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Export ("initWithDevice:a:b:")]
		NativeHandle Constructor (IMTLDevice device, float a, float b);

		// [Export ("initWithDevice:")] marked as NS_UNAVAILABLE - Use initWithDevice:a:b: instead

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:neuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSNNNeuronDescriptor neuronDescriptor);
	}

	/// <summary>The absolute-value activation function.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSCNNNeuronAbsolute">Apple documentation for <c>MPSCNNNeuronAbsolute</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuron), Name = "MPSCNNNeuronAbsolute")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronAbsolute {

		[Deprecated (PlatformName.TvOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:neuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSNNNeuronDescriptor neuronDescriptor);
	}

	/// <summary>Describes a convolution kernel.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSCNNConvolutionDescriptor">Apple documentation for <c>MPSCNNConvolutionDescriptor</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject), Name = "MPSCNNConvolutionDescriptor")]
	[DisableDefaultCtor]
	interface MPSCnnConvolutionDescriptor : NSCopying, NSSecureCoding {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("kernelWidth")]
		nuint KernelWidth { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("kernelHeight")]
		nuint KernelHeight { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("inputFeatureChannels")]
		nuint InputFeatureChannels { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("outputFeatureChannels")]
		nuint OutputFeatureChannels { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("strideInPixelsX")]
		nuint StrideInPixelsX { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("strideInPixelsY")]
		nuint StrideInPixelsY { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("groups")]
		nuint Groups { get; set; }

		/// <summary>To be added.</summary>
		///         <value>
		///           <para>(More documentation for this node is coming)</para>
		///           <para tool="nullallowed">This value can be <see langword="null" />.</para>
		///         </value>
		///         <remarks>To be added.</remarks>
		[NullAllowed, Export ("neuron", ArgumentSemantic.Retain)]
		[Deprecated (PlatformName.TvOS, 11, 0)]
		[Deprecated (PlatformName.iOS, 11, 0)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1)]
		MPSCnnNeuron Neuron { get; set; }

		[Static]
		[Deprecated (PlatformName.TvOS, 11, 0)]
		[Deprecated (PlatformName.iOS, 11, 0)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1)]
		[Export ("cnnConvolutionDescriptorWithKernelWidth:kernelHeight:inputFeatureChannels:outputFeatureChannels:neuronFilter:")]
		MPSCnnConvolutionDescriptor GetConvolutionDescriptor (nuint kernelWidth, nuint kernelHeight, nuint inputFeatureChannels, nuint outputFeatureChannels, [NullAllowed] MPSCnnNeuron neuronFilter);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Static]
		[Export ("supportsSecureCoding")]
		bool SupportsSecureCoding { get; }

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("cnnConvolutionDescriptorWithKernelWidth:kernelHeight:inputFeatureChannels:outputFeatureChannels:")]
		MPSCnnConvolutionDescriptor CreateCnnConvolutionDescriptor (nuint kernelWidth, nuint kernelHeight, nuint inputFeatureChannels, nuint outputFeatureChannels);

		[MacCatalyst (13, 1)]
		[Internal, Export ("setBatchNormalizationParametersForInferenceWithMean:variance:gamma:beta:epsilon:")]
		void SetBatchNormalizationParameters (IntPtr /* float* */ mean, IntPtr /* float* */ variance, [NullAllowed] IntPtr /* float* */ gamma, [NullAllowed] IntPtr /* float* */ beta, float epsilon);

		[Deprecated (PlatformName.TvOS, 11, 3, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[Deprecated (PlatformName.iOS, 11, 3, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 13, 4, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[MacCatalyst (13, 1)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[Export ("setNeuronType:parameterA:parameterB:")]
		void SetNeuronType (MPSCnnNeuronType neuronType, float parameterA, float parameterB);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Deprecated (PlatformName.TvOS, 11, 3, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[Deprecated (PlatformName.iOS, 11, 3, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 13, 4, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[MacCatalyst (13, 1)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[Export ("neuronType")]
		MPSCnnNeuronType NeuronType { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Deprecated (PlatformName.TvOS, 11, 3, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[Deprecated (PlatformName.iOS, 11, 3, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 13, 4, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[MacCatalyst (13, 1)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[Export ("neuronParameterA")]
		float NeuronParameterA { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Deprecated (PlatformName.TvOS, 11, 3, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[Deprecated (PlatformName.iOS, 11, 3, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 13, 4, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[MacCatalyst (13, 1)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[Export ("neuronParameterB")]
		float NeuronParameterB { get; }

		[Deprecated (PlatformName.TvOS, 11, 3, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[Deprecated (PlatformName.iOS, 11, 3, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 13, 4, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[MacCatalyst (13, 1)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'FusedNeuronDescriptor' property instead.")]
		[Export ("setNeuronToPReLUWithParametersA:")]
		void SetNeuronToPReLU (NSData A);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("dilationRateX")]
		nuint DilationRateX { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("dilationRateY")]
		nuint DilationRateY { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("fusedNeuronDescriptor", ArgumentSemantic.Retain)]
		MPSNNNeuronDescriptor FusedNeuronDescriptor { get; set; }
	}

	/// <summary>Developers should not use this deprecated class.</summary>
	[MacCatalyst (13, 1)]
	[Native]
	[Flags]
	public enum MPSCnnConvolutionFlags : ulong {
		/// <summary>To be added.</summary>
		None = 0,
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnKernel" /> that convolves its inputs, producing a feature map for each of its constituent filters.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSCNNConvolution">Apple documentation for <c>MPSCNNConvolution</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel), Name = "MPSCNNConvolution")]
	[DisableDefaultCtor]
	interface MPSCnnConvolution {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Override]
		[Export ("kernelWidth")]
		nuint KernelWidth { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Override]
		[Export ("kernelHeight")]
		nuint KernelHeight { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("inputFeatureChannels")]
		nuint InputFeatureChannels { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("outputFeatureChannels")]
		nuint OutputFeatureChannels { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Override]
		[Export ("strideInPixelsX")]
		nuint StrideInPixelsX { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Override]
		[Export ("strideInPixelsY")]
		nuint StrideInPixelsY { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("groups")]
		nuint Groups { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("dataSource", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource DataSource { get; }

		/// <summary>To be added.</summary>
		///         <value>
		///           <para>(More documentation for this node is coming)</para>
		///           <para tool="nullallowed">This value can be <see langword="null" />.</para>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Deprecated (PlatformName.TvOS, 11, 0, message: "A 'MPSCnnNeuron' is much too heavy for this purpose. Please set 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[Deprecated (PlatformName.iOS, 11, 0, message: "A 'MPSCnnNeuron' is much too heavy for this purpose. Please set 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "A 'MPSCnnNeuron' is much too heavy for this purpose. Please set 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[NullAllowed, Export ("neuron")]
		MPSCnnNeuron Neuron { get; }

		[Export ("initWithDevice:convolutionDescriptor:kernelWeights:biasTerms:flags:")]
		[Internal]
		IntPtr InitWithDevice (IMTLDevice device, MPSCnnConvolutionDescriptor convolutionDescriptor, /* float* */ IntPtr kernelWeights, [NullAllowed] /* float* */ IntPtr biasTerms, MPSCnnConvolutionFlags flags);

		// [Export ("initWithDevice:")] marked as NS_UNAVAILABLE - Use initWithDevice:convolutionDescriptor:kernelWeights:biasTerms instead

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:weights:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, IMPSCnnConvolutionDataSource weights);

		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("dilationRateX")]
		nuint DilationRateX { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("dilationRateY")]
		nuint DilationRateY { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("channelMultiplier")]
		nuint ChannelMultiplier { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("accumulatorPrecisionOption", ArgumentSemantic.Assign)]
		MPSNNConvolutionAccumulatorPrecisionOption AccumulatorPrecisionOption { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Deprecated (PlatformName.TvOS, 12, 0, message: "Use 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Use 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Use 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[MacCatalyst (13, 1)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[Export ("neuronType")]
		MPSCnnNeuronType NeuronType { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Deprecated (PlatformName.TvOS, 12, 0, message: "Use 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Use 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Use 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[MacCatalyst (13, 1)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[Export ("neuronParameterA")]
		float NeuronParameterA { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Deprecated (PlatformName.TvOS, 12, 0, message: "Use 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Use 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Use 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[MacCatalyst (13, 1)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[Export ("neuronParameterB")]
		float NeuronParameterB { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Deprecated (PlatformName.TvOS, 12, 0, message: "Use 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Use 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Use 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[MacCatalyst (13, 1)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use 'FusedNeuronDescriptor' property of convolution descriptor instead.")]
		[Export ("neuronParameterC")]
		float NeuronParameterC { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("subPixelScaleFactor")]
		nuint SubPixelScaleFactor { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("fusedNeuronDescriptor")]
		MPSNNNeuronDescriptor FusedNeuronDescriptor { get; }

		[MacCatalyst (13, 1)]
		[Export ("resultStateForSourceImage:sourceStates:destinationImage:")]
		[return: NullAllowed]
		MPSCnnConvolutionGradientState GetResultState (MPSImage sourceImage, [NullAllowed] NSArray<MPSState> sourceStates, MPSImage destinationImage);

		[MacCatalyst (13, 1)]
		[Export ("resultStateBatchForSourceImage:sourceStates:destinationImage:")]
		[return: NullAllowed]
		MPSCnnConvolutionGradientState [] GetResultStateBatch (NSArray<MPSImage> sourceImage, [NullAllowed] NSArray<MPSState> [] sourceStates, NSArray<MPSImage> destinationImage);

		[MacCatalyst (13, 1)]
		[Export ("temporaryResultStateForCommandBuffer:sourceImage:sourceStates:destinationImage:")]
		[return: NullAllowed]
		MPSCnnConvolutionGradientState GetTemporaryResultState (IMTLCommandBuffer commandBuffer, MPSImage sourceImage, [NullAllowed] NSArray<MPSState> sourceStates, MPSImage destinationImage);

		[MacCatalyst (13, 1)]
		[Export ("temporaryResultStateBatchForCommandBuffer:sourceImage:sourceStates:destinationImage:")]
		[return: NullAllowed]
		MPSCnnConvolutionGradientState [] GetTemporaryResultStateBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceImage, [NullAllowed] NSArray<MPSState> [] sourceStates, NSArray<MPSImage> destinationImage);

		[MacCatalyst (13, 1)]
		[Export ("reloadWeightsAndBiasesFromDataSource")]
		void ReloadWeightsAndBiasesFromDataSource ();

		[Deprecated (PlatformName.TvOS, 12, 0, message: "Please use 'ReloadWeightsAndBiasesFromDataSource' instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Please use 'ReloadWeightsAndBiasesFromDataSource' instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Please use 'ReloadWeightsAndBiasesFromDataSource' instead.")]
		[MacCatalyst (13, 1)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Please use 'ReloadWeightsAndBiasesFromDataSource' instead.")]
		[Export ("reloadWeightsAndBiasesWithDataSource:")]
		void ReloadWeightsAndBiases (IMPSCnnConvolutionDataSource dataSource);

		[MacCatalyst (13, 1)]
		[Export ("reloadWeightsAndBiasesWithCommandBuffer:state:")]
		void ReloadWeightsAndBiases (IMTLCommandBuffer commandBuffer, MPSCnnConvolutionWeightsAndBiasesState state);

		[MacCatalyst (13, 1)]
		[Export ("exportWeightsAndBiasesWithCommandBuffer:resultStateCanBeTemporary:")]
		MPSCnnConvolutionWeightsAndBiasesState ExportWeightsAndBiases (IMTLCommandBuffer commandBuffer, bool resultStateCanBeTemporary);
	}

	/// <summary>A fully connected layer.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSCNNFullyConnected">Apple documentation for <c>MPSCNNFullyConnected</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnConvolution), Name = "MPSCNNFullyConnected")]
	[DisableDefaultCtor]
	interface MPSCnnFullyConnected {

		[Export ("initWithDevice:convolutionDescriptor:kernelWeights:biasTerms:flags:")]
		[Internal]
		IntPtr InitWithDevice (IMTLDevice device, MPSCnnConvolutionDescriptor fullyConnectedDescriptor, /* float* */ IntPtr kernelWeights, [NullAllowed] /* float* */ IntPtr biasTerms, MPSCnnConvolutionFlags flags);

		// [Export ("initWithDevice:")] marked as NS_UNAVAILABLE - Use initWithDevice:convolutionDescriptor:kernelWeights:biasTerms instead

		// inlining ctor from base class
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:weights:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, IMPSCnnConvolutionDataSource weights);
	}

	/// <summary>A subclass of <see cref="T:MetalPerformanceShaders.MPSCnnKernel" /> that sub-samples its input.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSCNNPooling">Apple documentation for <c>MPSCNNPooling</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel), Name = "MPSCNNPooling")]
	[DisableDefaultCtor]
	interface MPSCnnPooling {

		[Override]
		[Export ("kernelWidth")]
		nuint KernelWidth { get; }

		[Override]
		[Export ("kernelHeight")]
		nuint KernelHeight { get; }

		[Override]
		[Export ("strideInPixelsX")]
		nuint StrideInPixelsX { get; }

		[Override]
		[Export ("strideInPixelsY")]
		nuint StrideInPixelsY { get; }

		[Export ("initWithDevice:kernelWidth:kernelHeight:")]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight);

		[Export ("initWithDevice:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY);

		// [Export ("initWithDevice:")] marked as NS_UNAVAILABLE - Use initWithDevice:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY: instead

		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnPooling" /> subclass that performs max-pooling.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSCNNPoolingMax">Apple documentation for <c>MPSCNNPoolingMax</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnPooling), Name = "MPSCNNPoolingMax")]
	[DisableDefaultCtor]
	interface MPSCnnPoolingMax {

		// inlining .ctor from base class

		[Export ("initWithDevice:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY);

		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>An average pooling filter.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSCNNPoolingAverage">Apple documentation for <c>MPSCNNPoolingAverage</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnPooling), Name = "MPSCNNPoolingAverage")]
	[DisableDefaultCtor]
	interface MPSCnnPoolingAverage {

		// inlining .ctor from base class

		[Export ("initWithDevice:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY);

		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[MacCatalyst (13, 1)]
		[Export ("zeroPadSizeX")]
		nuint ZeroPadSizeX { get; set; }

		[MacCatalyst (13, 1)]
		[Export ("zeroPadSizeY")]
		nuint ZeroPadSizeY { get; set; }
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnKernel" /> that performs spatial normalization.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSCNNSpatialNormalization">Apple documentation for <c>MPSCNNSpatialNormalization</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel), Name = "MPSCNNSpatialNormalization")]
	[DisableDefaultCtor]
	interface MPSCnnSpatialNormalization {

		[Export ("alpha")]
		float Alpha { get; set; }

		[Export ("beta")]
		float Beta { get; set; }

		[Export ("delta")]
		float Delta { get; set; }

		[Override]
		[Export ("kernelWidth")]
		nuint KernelWidth { get; }

		[Override]
		[Export ("kernelHeight")]
		nuint KernelHeight { get; }

		[Export ("initWithDevice:kernelWidth:kernelHeight:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight);

		// [Export ("initWithDevice:")] marked as NS_UNAVAILABLE - Use initWithDevice:kernelWidth:kernelHeight instead

		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnGradientKernel), Name = "MPSCNNSpatialNormalizationGradient")]
	[DisableDefaultCtor]
	interface MPSCnnSpatialNormalizationGradient {

		[Export ("alpha")]
		float Alpha { get; set; }

		[Export ("beta")]
		float Beta { get; set; }

		[Export ("delta")]
		float Delta { get; set; }

		[Export ("initWithDevice:kernelWidth:kernelHeight:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	/// <summary>A local contrast <see cref="T:MetalPerformanceShaders.MPSCnnKernel" />.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSCNNLocalContrastNormalization">Apple documentation for <c>MPSCNNLocalContrastNormalization</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel), Name = "MPSCNNLocalContrastNormalization")]
	[DisableDefaultCtor]
	interface MPSCnnLocalContrastNormalization {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("alpha")]
		float Alpha { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("beta")]
		float Beta { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("delta")]
		float Delta { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("p0")]
		float P0 { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("pm")]
		float Pm { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("ps")]
		float Ps { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Override]
		[Export ("kernelWidth")]
		nuint KernelWidth { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Override]
		[Export ("kernelHeight")]
		nuint KernelHeight { get; }

		[Export ("initWithDevice:kernelWidth:kernelHeight:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight);

		// [Export ("initWithDevice:")] marked as NS_UNAVAILABLE - Use initWithDevice:kernelWidth:kernelHeight instead

		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnGradientKernel), Name = "MPSCNNLocalContrastNormalizationGradient")]
	[DisableDefaultCtor]
	interface MPSCnnLocalContrastNormalizationGradient {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("alpha")]
		float Alpha { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("beta")]
		float Beta { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("delta")]
		float Delta { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("p0")]
		float P0 { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("pm")]
		float Pm { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("ps")]
		float Ps { get; set; }

		[Export ("initWithDevice:kernelWidth:kernelHeight:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnKernel" /> that normalizes across feature channels.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSCNNCrossChannelNormalization">Apple documentation for <c>MPSCNNCrossChannelNormalization</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel), Name = "MPSCNNCrossChannelNormalization")]
	[DisableDefaultCtor]
	interface MPSCnnCrossChannelNormalization {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("alpha")]
		float Alpha { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("beta")]
		float Beta { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("delta")]
		float Delta { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("kernelSize")]
		nuint KernelSize { get; }

		[Export ("initWithDevice:kernelSize:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelSize);

		// [Export ("initWithDevice:")] marked as NS_UNAVAILABLE - Use initWithDevice:kernelSize: instead

		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnGradientKernel), Name = "MPSCNNCrossChannelNormalizationGradient")]
	[DisableDefaultCtor]
	interface MPSCnnCrossChannelNormalizationGradient {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("alpha")]
		float Alpha { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("beta")]
		float Beta { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("delta")]
		float Delta { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("kernelSize")]
		nuint KernelSize { get; }

		[Export ("initWithDevice:kernelSize:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelSize);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnKernel" /> commonly used in categorization tasks.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSCNNSoftMax">Apple documentation for <c>MPSCNNSoftMax</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel), Name = "MPSCNNSoftMax")]
	[DisableDefaultCtor]
	interface MPSCnnSoftMax {

		// inlining .ctor from base class

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnGradientKernel), Name = "MPSCNNSoftMaxGradient")]
	[DisableDefaultCtor]
	interface MPSCnnSoftMaxGradient {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>The logarithmic softmax loss function.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSCNNLogSoftMax">Apple documentation for <c>MPSCNNLogSoftMax</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel), Name = "MPSCNNLogSoftMax")]
	[DisableDefaultCtor]
	interface MPSCnnLogSoftMax {

		// inlining .ctor from base class

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnGradientKernel), Name = "MPSCNNLogSoftMaxGradient")]
	[DisableDefaultCtor]
	interface MPSCnnLogSoftMaxGradient {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	// MPSImage.h

	/// <summary>Contains the attributes for a new or existing <see cref="T:MetalPerformanceShaders.MPSImage" />.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSImageDescriptor">Apple documentation for <c>MPSImageDescriptor</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MPSImageDescriptor : NSCopying {

		[Export ("width")]
		nuint Width { get; set; }

		[Export ("height")]
		nuint Height { get; set; }

		[Export ("featureChannels")]
		nuint FeatureChannels { get; set; }

		[Export ("numberOfImages")]
		nuint NumberOfImages { get; set; }

		[Export ("pixelFormat")]
		MTLPixelFormat PixelFormat { get; }

		[Export ("channelFormat", ArgumentSemantic.Assign)]
		MPSImageFeatureChannelFormat ChannelFormat { get; set; }

		[Export ("cpuCacheMode", ArgumentSemantic.Assign)]
		MTLCpuCacheMode CpuCacheMode { get; set; }

		[Export ("storageMode", ArgumentSemantic.Assign)]
		MTLStorageMode StorageMode { get; set; }

		[Export ("usage", ArgumentSemantic.Assign)]
		MTLTextureUsage Usage { get; set; }

		[Static]
		[Export ("imageDescriptorWithChannelFormat:width:height:featureChannels:")]
		MPSImageDescriptor GetImageDescriptor (MPSImageFeatureChannelFormat channelFormat, nuint width, nuint height, nuint featureChannels);

		[Static]
		[Export ("imageDescriptorWithChannelFormat:width:height:featureChannels:numberOfImages:usage:")]
		MPSImageDescriptor GetImageDescriptor (MPSImageFeatureChannelFormat channelFormat, nuint width, nuint height, nuint featureChannels, nuint numberOfImages, MTLTextureUsage usage);
	}

	/// <summary>Enumerates an image's underlying texture's purgeable state.</summary>
	[MacCatalyst (13, 1)]
	[Native]
	[Flags]
	public enum MPSPurgeableState : ulong {
		AllocationDeferred = 0,
		KeepCurrent = MTLPurgeableState.KeepCurrent,
		NonVolatile = MTLPurgeableState.NonVolatile,
		Volatile = MTLPurgeableState.Volatile,
		Empty = MTLPurgeableState.Empty,
	}

	/// <summary>An image that may contain more than 4 channels. (For example, an image in a layer of a convolutional neural network.)</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSImage">Apple documentation for <c>MPSImage</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MPSImage {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Static]
		[Export ("defaultAllocator")]
		IMPSImageAllocator DefaultAllocator { get; }

		/// <summary>Gets the Metal device for the image.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("device", ArgumentSemantic.Retain)]
		IMTLDevice Device { get; }

		/// <summary>Gets the height of the image.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("width")]
		nuint Width { get; }

		/// <summary>Gets the height of the image.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("height")]
		nuint Height { get; }

		/// <summary>Gets the number of feature channels for the image.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("featureChannels")]
		nuint FeatureChannels { get; }

		/// <summary>Gets the number of images in a batch of images for processing.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("numberOfImages")]
		nuint NumberOfImages { get; }

		/// <summary>Gets the type of the underlying texture for the image.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("textureType")]
		MTLTextureType TextureType { get; }

		/// <summary>Gets pixel format of the image texture.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("pixelFormat")]
		MTLPixelFormat PixelFormat { get; }

		/// <summary>Gets the number of bits of precision for each feature channel.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("precision")]
		nuint Precision { get; }

		/// <summary>Gets the intended use of the image's underlying texture.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("usage")]
		MTLTextureUsage Usage { get; }

		[TV (13, 0), iOS (13, 0)]
		[MacCatalyst (13, 1)]
		[Export ("featureChannelFormat")]
		MPSImageFeatureChannelFormat FeatureChannelFormat { get; }

		/// <summary>Gets the number of bytes that are between the start of each pixel and the start of the next sequential pixel. (The stride for one pixel, in bytes.)</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("pixelSize")]
		nuint PixelSize { get; }

		/// <summary>Gets the underlying texture for the image.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("texture")]
		IMTLTexture Texture { get; }

		/// <summary>Gets or sets the image object's label.</summary>
		///         <value>
		///           <para>(More documentation for this node is coming)</para>
		///           <para tool="nullallowed">This value can be <see langword="null" />.</para>
		///         </value>
		///         <remarks>To be added.</remarks>
		[NullAllowed, Export ("label")]
		string Label { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("parent", ArgumentSemantic.Retain)]
		MPSImage Parent { get; }

		[Export ("initWithDevice:imageDescriptor:")]
		NativeHandle Constructor (IMTLDevice device, MPSImageDescriptor imageDescriptor);

		[DesignatedInitializer]
		[MacCatalyst (13, 1)]
		[Export ("initWithParentImage:sliceRange:featureChannels:")]
		NativeHandle Constructor (MPSImage parent, NSRange sliceRange, nuint featureChannels);

		[Export ("initWithTexture:featureChannels:")]
		NativeHandle Constructor (IMTLTexture texture, nuint featureChannels);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("batchRepresentation")]
		NSArray<MPSImage> BatchRepresentation { get; }

		[MacCatalyst (13, 1)]
		[Export ("batchRepresentationWithSubRange:")]
		NSArray<MPSImage> GetBatchRepresentation (NSRange subRange);

		[MacCatalyst (13, 1)]
		[Export ("subImageWithFeatureChannelRange:")]
		MPSImage GetSubImage (NSRange featureChannelRange);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("resourceSize")]
		nuint ResourceSize { get; }

		[Export ("setPurgeableState:")]
		MPSPurgeableState SetPurgeableState (MPSPurgeableState state);

		[MacCatalyst (13, 1)]
		[Export ("readBytes:dataLayout:bytesPerRow:region:featureChannelInfo:imageIndex:")]
		void ReadBytes (IntPtr /* void* */ dataBytes, MPSDataLayout dataLayout, nuint bytesPerRow, MTLRegion region, MPSImageReadWriteParams featureChannelInfo, nuint imageIndex);

		[MacCatalyst (13, 1)]
		[Export ("writeBytes:dataLayout:bytesPerRow:region:featureChannelInfo:imageIndex:")]
		void WriteBytes (IntPtr /* void* */ dataBytes, MPSDataLayout dataLayout, nuint bytesPerRow, MTLRegion region, MPSImageReadWriteParams featureChannelInfo, nuint imageIndex);

		[TV (13, 0), iOS (13, 0)]
		[MacCatalyst (13, 1)]
		[Export ("writeBytes:dataLayout:bytesPerColumn:bytesPerRow:bytesPerImage:region:featureChannelInfo:imageIndex:")]
		void WriteBytes (IntPtr /* void* */ dataBytes, MPSDataLayout dataLayout, nuint bytesPerColumn, nuint bytesPerRow, nuint bytesPerImage, MTLRegion region, MPSImageReadWriteParams featureChannelInfo, nuint imageIndex);

		[MacCatalyst (13, 1)]
		[Export ("readBytes:dataLayout:bytesPerRow:bytesPerImage:region:featureChannelInfo:imageIndex:")]
		void ReadBytes (IntPtr /* void* */ dataBytes, MPSDataLayout dataLayout, nuint bytesPerRow, nuint bytesPerImage, MTLRegion region, MPSImageReadWriteParams featureChannelInfo, nuint imageIndex);

		[MacCatalyst (13, 1)]
		[Export ("writeBytes:dataLayout:bytesPerRow:bytesPerImage:region:featureChannelInfo:imageIndex:")]
		void WriteBytes (IntPtr /* void* */ dataBytes, MPSDataLayout dataLayout, nuint bytesPerRow, nuint bytesPerImage, MTLRegion region, MPSImageReadWriteParams featureChannelInfo, nuint imageIndex);

		[MacCatalyst (13, 1)]
		[Export ("readBytes:dataLayout:imageIndex:")]
		void ReadBytes (IntPtr /* void* */ dataBytes, MPSDataLayout dataLayout, nuint imageIndex);

		[MacCatalyst (13, 1)]
		[Export ("writeBytes:dataLayout:imageIndex:")]
		void WriteBytes (IntPtr /* void* */ dataBytes, MPSDataLayout dataLayout, nuint imageIndex);

		[MacCatalyst (13, 1)]
		[Export ("synchronizeOnCommandBuffer:")]
		void Synchronize (IMTLCommandBuffer commandBuffer);
	}

	/// <summary>Discardable image data.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSTemporaryImage">Apple documentation for <c>MPSTemporaryImage</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImage))]
	[DisableDefaultCtor]
	interface MPSTemporaryImage {

		[MacCatalyst (13, 1)]
		[Static]
		[New]
		[Export ("defaultAllocator")]
		IMPSImageAllocator DefaultAllocator { get; }

		[DesignatedInitializer]
		[MacCatalyst (13, 1)]
		[Export ("initWithParentImage:sliceRange:featureChannels:")]
		NativeHandle Constructor (MPSImage parent, NSRange sliceRange, nuint featureChannels);

		[Static]
		[Export ("temporaryImageWithCommandBuffer:imageDescriptor:")]
		MPSTemporaryImage GetTemporaryImage (IMTLCommandBuffer commandBuffer, MPSImageDescriptor imageDescriptor);

		[Static]
		[Export ("temporaryImageWithCommandBuffer:textureDescriptor:")]
		MPSTemporaryImage GetTemporaryImage (IMTLCommandBuffer commandBuffer, MTLTextureDescriptor textureDescriptor);

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("temporaryImageWithCommandBuffer:textureDescriptor:featureChannels:")]
		MPSTemporaryImage GetTemporaryImage (IMTLCommandBuffer commandBuffer, MTLTextureDescriptor textureDescriptor, nuint featureChannels);

		[Static]
		[Export ("prefetchStorageWithCommandBuffer:imageDescriptorList:")]
		void PrefetchStorage (IMTLCommandBuffer commandBuffer, MPSImageDescriptor [] descriptorList);

		[Export ("readCount")]
		nuint ReadCount { get; set; }
	}

	[TV (13, 0), iOS (13, 0)]
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	interface MPSPredicate {
		[Export ("predicateBuffer", ArgumentSemantic.Retain)]
		IMTLBuffer PredicateBuffer { get; }

		[Export ("predicateOffset")]
		nuint PredicateOffset { get; }

		[Static]
		[Export ("predicateWithBuffer:offset:")]
		MPSPredicate Create (IMTLBuffer buffer, nuint offset);

		[Export ("initWithBuffer:offset:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLBuffer buffer, nuint offset);

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	interface IMPSHeapProvider { }

	[Protocol]
	interface MPSHeapProvider {
		[Abstract]
		[Export ("newHeapWithDescriptor:")]
		[return: NullAllowed]
		[return: Release]
		IMTLHeap GetNewHeap (MTLHeapDescriptor descriptor);

		[Export ("retireHeap:cacheDelay:")]
		void RetireHeap (IMTLHeap heap, double seconds);
	}

	[TV (13, 0), iOS (13, 0)]
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MPSCommandBuffer : MTLCommandBuffer {
		[Export ("commandBuffer", ArgumentSemantic.Retain)]
		IMTLCommandBuffer CommandBuffer { get; }

		[Export ("rootCommandBuffer", ArgumentSemantic.Retain)]
		IMTLCommandBuffer RootCommandBuffer { get; }

		[NullAllowed, Export ("predicate", ArgumentSemantic.Retain)]
		MPSPredicate Predicate { get; set; }

		[NullAllowed, Export ("heapProvider", ArgumentSemantic.Retain)]
		IMPSHeapProvider HeapProvider { get; set; }

		[Static]
		[Export ("commandBufferWithCommandBuffer:")]
		MPSCommandBuffer Create (IMTLCommandBuffer commandBuffer);

		[Static]
		[Export ("commandBufferFromCommandQueue:")]
		MPSCommandBuffer Create (IMTLCommandQueue commandQueue);

		[Export ("initWithCommandBuffer:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLCommandBuffer commandBuffer);

		[Export ("commitAndContinue")]
		void CommitAndContinue ();

		[Export ("prefetchHeapForWorkloadSize:")]
		void PrefetchHeap (nuint size);
	}


	// MPSImageConversion.h

	/// <summary>An image that can convert its color space, alpha, or pixel format.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSImageConversion">Apple documentation for <c>MPSImageConversion</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageConversion {

		/// <summary>Gets the premultiplication settings for the source image.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("sourceAlpha")]
		MPSAlphaType SourceAlpha { get; }

		/// <summary>Gets the premultiplication settings for the destination image.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("destinationAlpha")]
		MPSAlphaType DestinationAlpha { get; }

		[Export ("initWithDevice:srcAlpha:destAlpha:backgroundColor:conversionInfo:")]
		[Internal]
		IntPtr InitWithDevice (IMTLDevice device, MPSAlphaType srcAlpha, MPSAlphaType destAlpha, [NullAllowed] /* nfloat* */ IntPtr backgroundColor, [NullAllowed] CGColorConversionInfo conversionInfo);

		// inlining ctor from base class
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	// MPSMatrix.h

	/// <summary>Describes the size, data type, and stride of a row-major matrix.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSMatrixDescriptor">Apple documentation for <c>MPSMatrixDescriptor</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MPSMatrixDescriptor {

		[Export ("rows")]
		nuint Rows { get; set; }

		[Export ("columns")]
		nuint Columns { get; set; }

		[Export ("dataType", ArgumentSemantic.Assign)]
		MPSDataType DataType { get; set; }

		[Export ("rowBytes")]
		nuint RowBytes { get; set; }

		[Static]
		[Deprecated (PlatformName.TvOS, 11, 0)]
		[Deprecated (PlatformName.iOS, 11, 0)]
		[Unavailable (PlatformName.MacCatalyst)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1)]
		[Export ("matrixDescriptorWithDimensions:columns:rowBytes:dataType:")]
		MPSMatrixDescriptor Create (nuint rows, nuint columns, nuint rowBytes, MPSDataType dataType);

		[Static]
		[Deprecated (PlatformName.iOS, 11, 0)]
		[Deprecated (PlatformName.TvOS, 11, 0)]
		[Unavailable (PlatformName.MacCatalyst)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1)]
		[Export ("rowBytesFromColumns:dataType:")]
		nuint GetRowBytesFromColumns (nuint columns, MPSDataType dataType);

		[MacCatalyst (13, 1)]
		[Export ("matrices")]
		nuint Matrices { get; }

		[MacCatalyst (13, 1)]
		[Export ("matrixBytes")]
		nuint MatrixBytes { get; }

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("matrixDescriptorWithRows:columns:rowBytes:dataType:")]
		MPSMatrixDescriptor GetMatrixDescriptor (nuint rows, nuint columns, nuint rowBytes, MPSDataType dataType);

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("matrixDescriptorWithRows:columns:matrices:rowBytes:matrixBytes:dataType:")]
		MPSMatrixDescriptor GetMatrixDescriptor (nuint rows, nuint columns, nuint matrices, nuint rowBytes, nuint matrixBytes, MPSDataType dataType);

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("rowBytesForColumns:dataType:")]
		nuint GetRowBytesForColumns (nuint columns, MPSDataType dataType);
	}

	/// <summary>A matrix that represents the kernel of a linear transformation.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSMatrix">Apple documentation for <c>MPSMatrix</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor] // init NS_UNAVAILABLE;
	interface MPSMatrix {

		[Export ("device", ArgumentSemantic.Retain)]
		IMTLDevice Device { get; }

		[Export ("rows")]
		nuint Rows { get; }

		[Export ("columns")]
		nuint Columns { get; }

		[Export ("dataType")]
		MPSDataType DataType { get; }

		[Export ("rowBytes")]
		nuint RowBytes { get; }

		[Export ("data")]
		IMTLBuffer Data { get; }

		[Export ("initWithBuffer:descriptor:")]
		NativeHandle Constructor (IMTLBuffer buffer, MPSMatrixDescriptor descriptor);

		[TV (13, 0), iOS (13, 0)]
		[MacCatalyst (13, 1)]
		[Export ("initWithBuffer:offset:descriptor:")]
		NativeHandle Constructor (IMTLBuffer buffer, nuint offset, MPSMatrixDescriptor descriptor);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:descriptor:")]
		NativeHandle Constructor (IMTLDevice device, MPSMatrixDescriptor descriptor);

		[MacCatalyst (13, 1)]
		[Export ("synchronizeOnCommandBuffer:")]
		void Synchronize (IMTLCommandBuffer commandBuffer);

		[MacCatalyst (13, 1)]
		[Export ("resourceSize")]
		nuint ResourceSize { get; }

		[MacCatalyst (13, 1)]
		[Export ("matrices")]
		nuint Matrices { get; }

		[MacCatalyst (13, 1)]
		[Export ("matrixBytes")]
		nuint MatrixBytes { get; }

		[TV (13, 0), iOS (13, 1)]
		[MacCatalyst (13, 1)]
		[Export ("offset")]
		nuint Offset { get; }
	}

	// MPSMatrixMultiplication.h

	/// <summary>Represents a weighted matrix multiplication operation, followed by a weighted addition operation.</summary>
	///     
	///     <related type="externalDocumentation" href="https://developer.apple.com/reference/MetalPerformanceShaders/MPSMatrixMultiplication">Apple documentation for <c>MPSMatrixMultiplication</c></related>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel))]
	[DisableDefaultCtor]
	interface MPSMatrixMultiplication {
		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:resultRows:resultColumns:interiorColumns:")]
		NativeHandle Constructor (IMTLDevice device, nuint resultRows, nuint resultColumns, nuint interiorColumns);

		[Export ("resultMatrixOrigin", ArgumentSemantic.Assign)]
		MTLOrigin ResultMatrixOrigin { get; set; }

		[Export ("leftMatrixOrigin", ArgumentSemantic.Assign)]
		MTLOrigin LeftMatrixOrigin { get; set; }

		[Export ("rightMatrixOrigin", ArgumentSemantic.Assign)]
		MTLOrigin RightMatrixOrigin { get; set; }

		[Export ("initWithDevice:transposeLeft:transposeRight:resultRows:resultColumns:interiorColumns:alpha:beta:")]
		NativeHandle Constructor (IMTLDevice device, bool transposeLeft, bool transposeRight, nuint resultRows, nuint resultColumns, nuint interiorColumns, double alpha, double beta);

		[Export ("encodeToCommandBuffer:leftMatrix:rightMatrix:resultMatrix:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSMatrix leftMatrix, MPSMatrix rightMatrix, MPSMatrix resultMatrix);

		// [Export ("initWithDevice:")] marked as NS_UNAVAILABLE - Use the above initialization method instead.

		// inlining ctor from base class
		[MacCatalyst (13, 1)]
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[MacCatalyst (13, 1)]
		[Export ("batchStart")]
		nuint BatchStart { get; set; }

		[MacCatalyst (13, 1)]
		[Export ("batchSize")]
		nuint BatchSize { get; set; }
	}

	[TV (13, 0), Mac (10, 15), iOS (13, 0), MacCatalyst (13, 0)]
	[BaseType (typeof (MPSNDArrayMultiaryKernel))]
	[DisableDefaultCtor]
	interface MPSNDArrayMatrixMultiplication {
		[Export ("alpha")]
		double Alpha { get; set; }

		[Export ("beta")]
		double Beta { get; set; }
	}

	/// <summary>Temporary storage used by convolutional neural nets.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MPSState {

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("temporaryStateWithCommandBuffer:bufferSize:")]
		MPSState CreateTemporaryState (IMTLCommandBuffer commandBuffer, nuint bufferSize);

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("temporaryStateWithCommandBuffer:textureDescriptor:")]
		MPSState CreateTemporaryState (IMTLCommandBuffer commandBuffer, MTLTextureDescriptor descriptor);

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("temporaryStateWithCommandBuffer:")]
		MPSState CreateTemporaryState (IMTLCommandBuffer commandBuffer);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:bufferSize:")]
		NativeHandle Constructor (IMTLDevice device, nuint bufferSize);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:textureDescriptor:")]
		NativeHandle Constructor (IMTLDevice device, MTLTextureDescriptor descriptor);

		[MacCatalyst (13, 1)]
		[Export ("initWithResource:")]
		NativeHandle Constructor ([NullAllowed] IMTLResource resource);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:resourceList:")]
		NativeHandle Constructor (IMTLDevice device, MPSStateResourceList resourceList);

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("temporaryStateWithCommandBuffer:resourceList:")]
		MPSState CreateTemporaryState (IMTLCommandBuffer commandBuffer, MPSStateResourceList resourceList);

		[MacCatalyst (13, 1)]
		[Export ("initWithResources:")]
		NativeHandle Constructor ([NullAllowed] IMTLResource [] resources);

		[MacCatalyst (13, 1)]
		[Export ("resourceCount")]
		nuint ResourceCount { get; }

		[MacCatalyst (13, 1)]
		[Export ("resourceAtIndex:allocateMemory:")]
		[return: NullAllowed]
		IMTLResource GetResource (nuint index, bool allocateMemory);

		[Export ("readCount")]
		nuint ReadCount { get; set; }

		[Export ("isTemporary")]
		bool IsTemporary { get; }

		[NullAllowed, Export ("label")]
		string Label { get; set; }

		[MacCatalyst (13, 1)]
		[Export ("bufferSizeAtIndex:")]
		nuint GetBufferSize (nuint index);

		[MacCatalyst (13, 1)]
		[Export ("textureInfoAtIndex:")]
		MPSStateTextureInfo GetTextureInfo (nuint index);

		[MacCatalyst (13, 1)]
		[Export ("resourceTypeAtIndex:")]
		MPSStateResourceType GetResourceType (nuint index);

		[MacCatalyst (13, 1)]
		[Export ("synchronizeOnCommandBuffer:")]
		void Synchronize (IMTLCommandBuffer commandBuffer);

		[MacCatalyst (13, 1)]
		[Export ("resourceSize")]
		nuint ResourceSize { get; }

		[MacCatalyst (13, 1)]
		[Export ("destinationImageDescriptorForSourceImages:sourceStates:forKernel:suggestedDescriptor:")]
		MPSImageDescriptor GetDestinationImageDescriptor (NSArray<MPSImage> sourceImages, [NullAllowed] NSArray<MPSState> sourceStates, MPSKernel kernel, MPSImageDescriptor inDescriptor);

		[Deprecated (PlatformName.TvOS, 12, 0, message: "Please use 'GetResource (nuint, bool)' instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Please use 'GetResource (nuint, bool)' instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 13, 4, message: "Please use 'GetResource (nuint, bool)' instead.")]
		[Unavailable (PlatformName.MacCatalyst)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Please use 'GetResource (nuint, bool)' instead.")]
		[NullAllowed, Export ("resource", ArgumentSemantic.Retain)]
		IMTLResource Resource { get; }
	}

	/// <summary>A matrix allocated in GPU private memory.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrix))]
	[DisableDefaultCtor]
	interface MPSTemporaryMatrix {
		[Static]
		[Export ("temporaryMatrixWithCommandBuffer:matrixDescriptor:")]
		MPSTemporaryMatrix Create (IMTLCommandBuffer commandBuffer, MPSMatrixDescriptor matrixDescriptor);

		[Static]
		[Export ("prefetchStorageWithCommandBuffer:matrixDescriptorList:")]
		void PrefetchStorage (IMTLCommandBuffer commandBuffer, MPSMatrixDescriptor [] descriptorList);

		[Export ("readCount")]
		nuint ReadCount { get; set; }
	}

	/// <summary>A one-dimensional array.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MPSVector {
		[Export ("device", ArgumentSemantic.Retain)]
		IMTLDevice Device { get; }

		[Export ("length")]
		nuint Length { get; }

		[Export ("vectors")]
		nuint Vectors { get; }

		[Export ("dataType")]
		MPSDataType DataType { get; }

		[Export ("vectorBytes")]
		nuint VectorBytes { get; }

		[Export ("data")]
		IMTLBuffer Data { get; }

		[Export ("initWithBuffer:descriptor:")]
		NativeHandle Constructor (IMTLBuffer buffer, MPSVectorDescriptor descriptor);

		[TV (13, 0), iOS (13, 0)]
		[MacCatalyst (13, 1)]
		[Export ("initWithBuffer:offset:descriptor:")]
		NativeHandle Constructor (IMTLBuffer buffer, nuint offset, MPSVectorDescriptor descriptor);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:descriptor:")]
		NativeHandle Constructor (IMTLDevice device, MPSVectorDescriptor descriptor);

		[MacCatalyst (13, 1)]
		[Export ("synchronizeOnCommandBuffer:")]
		void Synchronize (IMTLCommandBuffer commandBuffer);

		[MacCatalyst (13, 1)]
		[Export ("resourceSize")]
		nuint ResourceSize { get; }

		[TV (13, 0), iOS (13, 1)]
		[MacCatalyst (13, 1)]
		[Export ("offset")]
		nuint Offset { get; }
	}

	/// <summary>Describes the length and data type of a <see cref="T:MetalPerformanceShaders.MPSVector" />.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MPSVectorDescriptor {
		[Export ("length")]
		nuint Length { get; set; }

		[Export ("vectors")]
		nuint Vectors { get; }

		[Export ("dataType", ArgumentSemantic.Assign)]
		MPSDataType DataType { get; set; }

		[Export ("vectorBytes")]
		nuint VectorBytes { get; }

		[Static]
		[Export ("vectorDescriptorWithLength:dataType:")]
		MPSVectorDescriptor Create (nuint length, MPSDataType dataType);

		[Static]
		[Export ("vectorDescriptorWithLength:vectors:vectorBytes:dataType:")]
		MPSVectorDescriptor Create (nuint length, nuint vectors, nuint vectorBytes, MPSDataType dataType);

		[Static]
		[Export ("vectorBytesForLength:dataType:")]
		nuint GetVectorBytes (nuint length, MPSDataType dataType);
	}

	/// <summary>A kernel that performs a mapping from one matrix to another.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel))]
	[DisableDefaultCtor] // According to docs needs a Metal Device so initWithDevice: makes more sense.
	interface MPSMatrixUnaryKernel {
		[Export ("sourceMatrixOrigin", ArgumentSemantic.Assign)]
		MTLOrigin SourceMatrixOrigin { get; set; }

		[Export ("resultMatrixOrigin", ArgumentSemantic.Assign)]
		MTLOrigin ResultMatrixOrigin { get; set; }

		[Export ("batchStart")]
		nuint BatchStart { get; set; }

		[Export ("batchSize")]
		nuint BatchSize { get; set; }

		// inlining ctor from base class
		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>A kernel that operates on two matrices to create a new matrix.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel))]
	[DisableDefaultCtor] // According to docs needs a Metal Device so initWithDevice: makes more sense.
	interface MPSMatrixBinaryKernel {
		[Export ("primarySourceMatrixOrigin", ArgumentSemantic.Assign)]
		MTLOrigin PrimarySourceMatrixOrigin { get; set; }

		[Export ("secondarySourceMatrixOrigin", ArgumentSemantic.Assign)]
		MTLOrigin SecondarySourceMatrixOrigin { get; set; }

		[Export ("resultMatrixOrigin", ArgumentSemantic.Assign)]
		MTLOrigin ResultMatrixOrigin { get; set; }

		[Export ("batchStart")]
		nuint BatchStart { get; set; }

		[Export ("batchSize")]
		nuint BatchSize { get; set; }

		// inlining ctor from base class
		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>Performs matrix multiplication.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixBinaryKernel))]
	[DisableDefaultCtor] // According to docs needs a Metal Device so initWithDevice: makes more sense.
	interface MPSMatrixVectorMultiplication {
		[Export ("initWithDevice:transpose:rows:columns:alpha:beta:")]
		NativeHandle Constructor (IMTLDevice device, bool transpose, nuint rows, nuint columns, double alpha, double beta);

		[Export ("initWithDevice:rows:columns:")]
		NativeHandle Constructor (IMTLDevice device, nuint rows, nuint columns);

		[Export ("encodeToCommandBuffer:inputMatrix:inputVector:resultVector:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSMatrix inputMatrix, MPSVector inputVector, MPSVector resultVector);

		// inlining ctor from base class
		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSMatrixBinaryKernel" /> that solves a linear system of equations via a triangular coefficient matrix.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixBinaryKernel))]
	[DisableDefaultCtor] // According to docs needs a Metal Device so initWithDevice: makes more sense.
	interface MPSMatrixSolveTriangular {
		[Export ("initWithDevice:right:upper:transpose:unit:order:numberOfRightHandSides:alpha:")]
		NativeHandle Constructor (IMTLDevice device, bool right, bool upper, bool transpose, bool unit, nuint order, nuint numberOfRightHandSides, double alpha);

		[Export ("encodeToCommandBuffer:sourceMatrix:rightHandSideMatrix:solutionMatrix:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSMatrix sourceMatrix, MPSMatrix rightHandSideMatrix, MPSMatrix solutionMatrix);

		// inlining ctor from base class
		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixBinaryKernel))]
	[DisableDefaultCtor] // According to docs needs a Metal Device so initWithDevice: makes more sense.
	interface MPSMatrixSolveLU {
		[Export ("initWithDevice:transpose:order:numberOfRightHandSides:")]
		NativeHandle Constructor (IMTLDevice device, bool transpose, nuint order, nuint numberOfRightHandSides);

		[Export ("encodeToCommandBuffer:sourceMatrix:rightHandSideMatrix:pivotIndices:solutionMatrix:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSMatrix sourceMatrix, MPSMatrix rightHandSideMatrix, MPSMatrix pivotIndices, MPSMatrix solutionMatrix);

		// inlining ctor from base class
		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSMatrixBinaryKernel" /> that solves a linear system of equations via Cholesky factorization.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixBinaryKernel))]
	[DisableDefaultCtor] // According to docs needs a Metal Device so initWithDevice: makes more sense.
	interface MPSMatrixSolveCholesky {
		[Export ("initWithDevice:upper:order:numberOfRightHandSides:")]
		NativeHandle Constructor (IMTLDevice device, bool upper, nuint order, nuint numberOfRightHandSides);

		[Export ("encodeToCommandBuffer:sourceMatrix:rightHandSideMatrix:solutionMatrix:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSMatrix sourceMatrix, MPSMatrix rightHandSideMatrix, MPSMatrix solutionMatrix);

		// inlining ctor from base class
		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSMatrixUnaryKernel" /> that computes LU factorization using partial pivoting.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixUnaryKernel))]
	[DisableDefaultCtor] // According to docs needs a Metal Device so initWithDevice: makes more sense.
	interface MPSMatrixDecompositionLU {
		[Export ("initWithDevice:rows:columns:")]
		NativeHandle Constructor (IMTLDevice device, nuint rows, nuint columns);

		[Export ("encodeToCommandBuffer:sourceMatrix:resultMatrix:pivotIndices:status:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSMatrix sourceMatrix, MPSMatrix resultMatrix, MPSMatrix pivotIndices, [NullAllowed] IMTLBuffer status);

		// inlining ctor from base class
		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSMatrixUnaryKernel" /> that computes the Cholesky factorization.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixUnaryKernel))]
	[DisableDefaultCtor] // According to docs needs a Metal Device so initWithDevice: makes more sense.
	interface MPSMatrixDecompositionCholesky {
		[Export ("initWithDevice:lower:order:")]
		NativeHandle Constructor (IMTLDevice device, bool lower, nuint order);

		[Export ("encodeToCommandBuffer:sourceMatrix:resultMatrix:status:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSMatrix sourceMatrix, MPSMatrix resultMatrix, [NullAllowed] IMTLBuffer status);

		// inlining ctor from base class
		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>Describes multiple matrix copy operations.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MPSMatrixCopyDescriptor {
		[Static]
		[Export ("descriptorWithSourceMatrix:destinationMatrix:offsets:")]
		MPSMatrixCopyDescriptor Create (MPSMatrix sourceMatrix, MPSMatrix destinationMatrix, MPSMatrixCopyOffsets offsets);

		[Export ("initWithDevice:count:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint count);

		[Export ("setCopyOperationAtIndex:sourceMatrix:destinationMatrix:offsets:")]
		void SetCopyOperation (nuint index, MPSMatrix sourceMatrix, MPSMatrix destinationMatrix, MPSMatrixCopyOffsets offsets);

		[Export ("initWithSourceMatrices:destinationMatrices:offsetVector:offset:")]
		[DesignatedInitializer]
		NativeHandle Constructor (MPSMatrix [] sourceMatrices, MPSMatrix [] destinationMatrices, [NullAllowed] MPSVector offsets, nuint byteOffset);
	}

	/// <summary>Performs multiple matrix copy operations.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel))]
	[DisableDefaultCtor] // There is a DesignatedInitializer, file a bug if needed.
	interface MPSMatrixCopy {
		[Export ("initWithDevice:copyRows:copyColumns:sourcesAreTransposed:destinationsAreTransposed:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint copyRows, nuint copyColumns, bool areSourcesTransposed, bool areDestinationsTransposed);

		[Export ("copyRows")]
		nuint CopyRows { get; }

		[Export ("copyColumns")]
		nuint CopyColumns { get; }

		[Export ("sourcesAreTransposed")]
		bool AreSourcesTransposed { get; }

		[Export ("destinationsAreTransposed")]
		bool AreDestinationsTransposed { get; }

		[Export ("encodeToCommandBuffer:copyDescriptor:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer cmdBuf, MPSMatrixCopyDescriptor copyDescriptor);

		[MacCatalyst (13, 1)]
		[Export ("encodeToCommandBuffer:copyDescriptor:rowPermuteIndices:rowPermuteOffset:columnPermuteIndices:columnPermuteOffset:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSMatrixCopyDescriptor copyDescriptor, [NullAllowed] MPSVector rowPermuteIndices, nuint rowPermuteOffset, [NullAllowed] MPSVector columnPermuteIndices, nuint columnPermuteOffset);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	[TV (13, 0), iOS (13, 0)]
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	interface MPSMatrixRandomDistributionDescriptor : NSCopying {
		[Export ("distributionType", ArgumentSemantic.Assign)]
		MPSMatrixRandomDistribution DistributionType { get; set; }

		[Export ("minimum")]
		float Minimum { get; set; }

		[Export ("maximum")]
		float Maximum { get; set; }

		[Export ("mean")]
		float Mean { get; set; }

		[Export ("standardDeviation")]
		float StandardDeviation { get; set; }

		[Static]
		[Export ("uniformDistributionDescriptorWithMinimum:maximum:")]
		MPSMatrixRandomDistributionDescriptor CreateUniform (float minimum, float maximum);

		[Static]
		[Export ("defaultDistributionDescriptor")]
		MPSMatrixRandomDistributionDescriptor CreateDefault ();
	}

	[DisableDefaultCtor]
	[TV (13, 0), iOS (13, 0)]
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel))]
	interface MPSMatrixRandom {
		[Export ("destinationDataType")]
		MPSDataType DestinationDataType { get; }

		[Export ("distributionType")]
		MPSMatrixRandomDistribution DistributionType { get; }

		[Export ("batchStart")]
		nuint BatchStart { get; set; }

		[Export ("batchSize")]
		nuint BatchSize { get; set; }

		[Export ("encodeToCommandBuffer:destinationVector:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSVector destinationVector);

		[Export ("encodeToCommandBuffer:destinationMatrix:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSMatrix destinationMatrix);
	}

	[DisableDefaultCtor]
	[TV (13, 0), iOS (13, 0)]
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixRandom), Name = "MPSMatrixRandomMTGP32")]
	interface MPSMatrixRandomMtgp32 {
		[Export ("initWithDevice:")]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithDevice:destinationDataType:seed:distributionDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSDataType destinationDataType, nuint seed, MPSMatrixRandomDistributionDescriptor distributionDescriptor);

		[Export ("synchronizeStateOnCommandBuffer:")]
		void Synchronize (IMTLCommandBuffer commandBuffer);

		[Export ("initWithDevice:destinationDataType:seed:")]
		NativeHandle Constructor (IMTLDevice device, MPSDataType destinationDataType, nuint seed);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	[DisableDefaultCtor]
	[TV (13, 0), iOS (13, 0)]
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixRandom))]
	interface MPSMatrixRandomPhilox {
		[Export ("initWithDevice:")]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithDevice:destinationDataType:seed:distributionDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSDataType destinationDataType, nuint seed, MPSMatrixRandomDistributionDescriptor distributionDescriptor);

		[Export ("initWithDevice:destinationDataType:seed:")]
		NativeHandle Constructor (IMTLDevice device, MPSDataType destinationDataType, nuint seed);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}


	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSKernel" /> that copies image data into a <see cref="T:MetalPerformanceShaders.MPSMatrix" /> object.</summary>
	[MacCatalyst (13, 1)]
	[DisableDefaultCtor]
	[BaseType (typeof (MPSKernel))]
	interface MPSImageCopyToMatrix {
		[Export ("destinationMatrixOrigin", ArgumentSemantic.Assign)]
		MTLOrigin DestinationMatrixOrigin { get; set; }

		[Export ("destinationMatrixBatchIndex")]
		nuint DestinationMatrixBatchIndex { get; set; }

		[Export ("dataLayout")]
		MPSDataLayout DataLayout { get; }

		[Export ("initWithDevice:dataLayout:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSDataLayout dataLayout);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("encodeToCommandBuffer:sourceImage:destinationMatrix:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSImage sourceImage, MPSMatrix destinationMatrix);

		[MacCatalyst (13, 1)]
		[Export ("encodeBatchToCommandBuffer:sourceImages:destinationMatrix:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceImages, MPSMatrix destinationMatrix);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel))]
	[DisableDefaultCtor] // There is a DesignatedInitializer, file a bug if needed.
	interface MPSImageFindKeypoints {
		[Export ("keypointRangeInfo")]
		MPSImageKeypointRangeInfo KeypointRangeInfo { get; }

		[Export ("initWithDevice:info:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSImageKeypointRangeInfo info);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("encodeToCommandBuffer:sourceTexture:regions:numberOfRegions:keypointCountBuffer:keypointCountBufferOffset:keypointDataBuffer:keypointDataBufferOffset:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, IMTLTexture source, MTLRegion regions, nuint numberOfRegions, IMTLBuffer keypointCountBuffer, nuint keypointCountBufferOffset, IMTLBuffer keypointDataBuffer, nuint keypointDataBufferOffset);
	}

	/// <summary>Base class for <see cref="T:MetalPerformanceShaders.MPSBinaryImageKernel" /> classes that perform arithmetic on images.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSBinaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageArithmetic {
		[Export ("primaryScale")]
		float PrimaryScale { get; set; }

		[Export ("secondaryScale")]
		float SecondaryScale { get; set; }

		[Export ("bias")]
		float Bias { get; set; }

		[Export ("primaryStrideInPixels", ArgumentSemantic.Assign)]
		MTLSize PrimaryStrideInPixels { get; set; }

		[Export ("secondaryStrideInPixels", ArgumentSemantic.Assign)]
		MTLSize SecondaryStrideInPixels { get; set; }

		// float
		[MacCatalyst (13, 1)]
		[Export ("minimumValue")]
		float MinimumValue { get; set; }

		// float
		[MacCatalyst (13, 1)]
		[Export ("maximumValue")]
		float MaximumValue { get; set; }

		//inlining ctor from base class
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSImageArithmetic" /> kernel that performs element-wise addition of two images.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageArithmetic))]
	[DisableDefaultCtor]
	interface MPSImageAdd {
		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSImageArithmetic" /> kernel that performs element-wise subtraction of two images.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageArithmetic))]
	[DisableDefaultCtor]
	interface MPSImageSubtract {
		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSImageArithmetic" /> kernel that performs element-wise multiplication of two images.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageArithmetic))]
	[DisableDefaultCtor]
	interface MPSImageMultiply {
		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSImageArithmetic" /> kernel that performs element-wise division of two images.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageArithmetic))]
	[DisableDefaultCtor]
	interface MPSImageDivide {
		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSUnaryImageKernel" /> that can resize and change aspect ratio of an image.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageScale {
		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		// scaleTransform property should be like:
		// unsafe MPSScaleTransform* ScaleTransform { get; set; }
		// which is both ugly and not supported by the generator
		[Export ("scaleTransform")]
		[Internal]
		IntPtr _GetScaleTransform ();

		[Export ("setScaleTransform:")]
		[Internal]
		void _SetScaleTransform (IntPtr value);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSImageScale" /> subclass that uses bilinear sampling to scale the image.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageScale))]
	[DisableDefaultCtor]
	interface MPSImageBilinearScale {
		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSUnaryImageKernel" /> that calculates the minimum and maximum pixel values for a region.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageStatisticsMinAndMax {
		[Export ("clipRectSource", ArgumentSemantic.Assign)]
		MTLRegion ClipRectSource { get; set; }

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSUnaryImageKernel" /> that calculates the mean and variance of pixel values for a region.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageStatisticsMeanAndVariance {
		[Export ("clipRectSource", ArgumentSemantic.Assign)]
		MTLRegion ClipRectSource { get; set; }

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSUnaryImageKernel" /> that calculates the mean of pixel values for a region.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageStatisticsMean {
		[Export ("clipRectSource", ArgumentSemantic.Assign)]
		MTLRegion ClipRectSource { get; set; }

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>Predefined common padding policies.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MPSNNDefaultPadding : MPSNNPadding {
		[Static]
		[Export ("paddingWithMethod:")]
		MPSNNDefaultPadding Create (MPSNNPaddingMethod method);

		[Static]
		[MacCatalyst (13, 1)]
		[Export ("paddingForTensorflowAveragePooling")]
		MPSNNDefaultPadding CreatePaddingForTensorflowAveragePooling ();

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("paddingForTensorflowAveragePoolingValidOnly")]
		MPSNNDefaultPadding CreatePaddingForTensorflowAveragePoolingValidOnly ();
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSKernel" /> that has binary weights.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel), Name = "MPSCNNBinaryKernel")]
	[DisableDefaultCtor]
	interface MPSCnnBinaryKernel {
		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("primaryOffset", ArgumentSemantic.Assign)]
		MPSOffset PrimaryOffset { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("secondaryOffset", ArgumentSemantic.Assign)]
		MPSOffset SecondaryOffset { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("clipRect", ArgumentSemantic.Assign)]
		MTLRegion ClipRect { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("destinationFeatureChannelOffset")]
		nuint DestinationFeatureChannelOffset { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("primarySourceFeatureChannelOffset")]
		nuint PrimarySourceFeatureChannelOffset { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("secondarySourceFeatureChannelOffset")]
		nuint SecondarySourceFeatureChannelOffset { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("primarySourceFeatureChannelMaxCount")]
		nuint PrimarySourceFeatureChannelMaxCount { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("secondarySourceFeatureChannelMaxCount")]
		nuint SecondarySourceFeatureChannelMaxCount { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("primaryEdgeMode", ArgumentSemantic.Assign)]
		MPSImageEdgeMode PrimaryEdgeMode { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("secondaryEdgeMode", ArgumentSemantic.Assign)]
		MPSImageEdgeMode SecondaryEdgeMode { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("primaryKernelWidth")]
		nuint PrimaryKernelWidth { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("primaryKernelHeight")]
		nuint PrimaryKernelHeight { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("secondaryKernelWidth")]
		nuint SecondaryKernelWidth { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("secondaryKernelHeight")]
		nuint SecondaryKernelHeight { get; }

#if !NET
		// Apple answered to radar://38054031 and said that these were exposed by mistake in an older release
		// and got removed because they are useless and no developers could have used it before.
		// Keeping stubs for binary compat.
		[Obsolete ("This was exposed by mistake, it will be removed in a future release.")]
		[Wrap ("0", IsVirtual = true)]
		nuint KernelWidth { get; }

		[Obsolete ("This was exposed by mistake, it will be removed in a future release.")]
		[Wrap ("0", IsVirtual = true)]
		nuint KernelHeight { get; }
#endif

		// Apple added availability info here
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("primaryStrideInPixelsX")]
		nuint PrimaryStrideInPixelsX { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("primaryStrideInPixelsY")]
		nuint PrimaryStrideInPixelsY { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("secondaryStrideInPixelsX")]
		nuint SecondaryStrideInPixelsX { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("secondaryStrideInPixelsY")]
		nuint SecondaryStrideInPixelsY { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("primaryDilationRateX")]
		nuint PrimaryDilationRateX { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("primaryDilationRateY")]
		nuint PrimaryDilationRateY { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("secondaryDilationRateX")]
		nuint SecondaryDilationRateX { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("secondaryDilationRateY")]
		nuint SecondaryDilationRateY { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("isBackwards")]
		bool IsBackwards { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("isStateModified")]
		bool IsStateModified { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("padding", ArgumentSemantic.Retain)]
		IMPSNNPadding Padding { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("destinationImageAllocator", ArgumentSemantic.Retain)]
		IMPSImageAllocator DestinationImageAllocator { get; set; }

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("encodeToCommandBuffer:primaryImage:secondaryImage:destinationImage:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSImage primaryImage, MPSImage secondaryImage, MPSImage destinationImage);

		[MacCatalyst (13, 1)]
		[Export ("encodeBatchToCommandBuffer:primaryImages:secondaryImages:destinationImages:")]
		void EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> primaryImages, NSArray<MPSImage> secondaryImages, NSArray<MPSImage> destinationImages);

		[Export ("encodeToCommandBuffer:primaryImage:secondaryImage:")]
		MPSImage EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSImage primaryImage, MPSImage secondaryImage);

		[MacCatalyst (13, 1)]
		[Export ("encodeBatchToCommandBuffer:primaryImages:secondaryImages:")]
		NSArray<MPSImage> EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> primaryImage, NSArray<MPSImage> secondaryImage);

		[MacCatalyst (13, 1)]
		[Export ("encodeToCommandBuffer:primaryImage:secondaryImage:destinationState:destinationStateIsTemporary:")]
		MPSImage EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSImage primaryImage, MPSImage secondaryImage, [NullAllowed] out MPSState outState, bool isTemporary);

		[MacCatalyst (13, 1)]
		[Export ("encodeBatchToCommandBuffer:primaryImages:secondaryImages:destinationStates:destinationStateIsTemporary:")]
		NSArray<MPSImage> EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> primaryImages, NSArray<MPSImage> secondaryImages, [NullAllowed] out MPSState [] outState, bool isTemporary);

		[MacCatalyst (13, 1)]
		[Export ("resultStateForPrimaryImage:secondaryImage:sourceStates:destinationImage:")]
		[return: NullAllowed]
		MPSState GetResultState (MPSImage primaryImage, MPSImage secondaryImage, [NullAllowed] NSArray<MPSState> sourceStates, MPSImage destinationImage);

		[MacCatalyst (13, 1)]
		[Export ("resultStateBatchForPrimaryImage:secondaryImage:sourceStates:destinationImage:")]
		[return: NullAllowed]
		NSArray<MPSState> GetResultStateBatch (NSArray<MPSImage> primaryImage, NSArray<MPSImage> secondaryImage, [NullAllowed] NSArray<MPSState> [] sourceStates, NSArray<MPSImage> destinationImage);

		[MacCatalyst (13, 1)]
		[Export ("temporaryResultStateForCommandBuffer:primaryImage:secondaryImage:sourceStates:destinationImage:")]
		[return: NullAllowed]
		MPSState GetTemporaryResultState (IMTLCommandBuffer commandBuffer, MPSImage primaryImage, MPSImage secondaryImage, [NullAllowed] NSArray<MPSState> sourceStates, MPSImage destinationImage);

		[MacCatalyst (13, 1)]
		[Export ("temporaryResultStateBatchForCommandBuffer:primaryImage:secondaryImage:sourceStates:destinationImage:")]
		[return: NullAllowed]
		NSArray<MPSState> GetTemporaryResultStateBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> primaryImage, NSArray<MPSImage> secondaryImage, [NullAllowed] NSArray<MPSState> [] sourceStates, NSArray<MPSImage> destinationImage);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("isResultStateReusedAcrossBatch")]
		bool IsResultStateReusedAcrossBatch { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("appendBatchBarrier")]
		bool AppendBatchBarrier { get; }

		[MacCatalyst (13, 1)]
		[Export ("destinationImageDescriptorForSourceImages:sourceStates:")]
		MPSImageDescriptor GetDestinationImageDescriptor (NSArray<MPSImage> sourceImages, [NullAllowed] NSArray<MPSState> sourceStates);
	}

	/// <summary>The parametric rectified linear unit activation function.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuron), Name = "MPSCNNNeuronPReLU")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronPReLU {
		[Export ("initWithDevice:a:count:")]
		[Internal, Sealed]
		IntPtr InitWithDevice (IMTLDevice device, IntPtr /* float* */ a, nuint count);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:neuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSNNNeuronDescriptor neuronDescriptor);

		// inlining ctor from base class
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>The hard sigmoid activation function.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuron), Name = "MPSCNNNeuronHardSigmoid")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronHardSigmoid {
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("a")]
		float A { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("b")]
		float B { get; }

		[Deprecated (PlatformName.TvOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Export ("initWithDevice:a:b:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, float a, float b);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:neuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSNNNeuronDescriptor neuronDescriptor);
	}

	/// <summary>The softplus activation function.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuron), Name = "MPSCNNNeuronSoftPlus")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronSoftPlus {
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("a")]
		float A { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("b")]
		float B { get; }

		[Deprecated (PlatformName.TvOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Export ("initWithDevice:a:b:")]
		NativeHandle Constructor (IMTLDevice device, float a, float b);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:neuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSNNNeuronDescriptor neuronDescriptor);
	}

	/// <summary>The softsign activation function.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuron), Name = "MPSCNNNeuronSoftSign")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronSoftSign {
		[Deprecated (PlatformName.TvOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:neuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSNNNeuronDescriptor neuronDescriptor);
	}

	/// <summary>The exponential linear unit activation function.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuron), Name = "MPSCNNNeuronELU")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronElu {
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("a")]
		float A { get; }

		[Deprecated (PlatformName.TvOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Export ("initWithDevice:a:")]
		NativeHandle Constructor (IMTLDevice device, float a);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:neuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSNNNeuronDescriptor neuronDescriptor);
	}

	/// <summary>The ReLUN activation function.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuron), Name = "MPSCNNNeuronReLUN")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronReLun {
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("a")]
		float A { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("b")]
		float B { get; }

		[Deprecated (PlatformName.TvOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Export ("initWithDevice:a:b:")]
		NativeHandle Constructor (IMTLDevice device, float a, float b);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:neuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSNNNeuronDescriptor neuronDescriptor);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuron), Name = "MPSCNNNeuronPower")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronPower {

		[Deprecated (PlatformName.TvOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Export ("initWithDevice:a:b:c:")]
		NativeHandle Constructor (IMTLDevice device, float a, float b, float c);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:neuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSNNNeuronDescriptor neuronDescriptor);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuron), Name = "MPSCNNNeuronExponential")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronExponential {

		[Deprecated (PlatformName.TvOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Export ("initWithDevice:a:b:c:")]
		NativeHandle Constructor (IMTLDevice device, float a, float b, float c);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:neuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSNNNeuronDescriptor neuronDescriptor);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuron), Name = "MPSCNNNeuronLogarithm")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronLogarithm {

		[Deprecated (PlatformName.TvOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Please use '.ctor (IMTLDevice, MPSNNNeuronDescriptor)' overload instead.")]
		[Export ("initWithDevice:a:b:c:")]
		NativeHandle Constructor (IMTLDevice device, float a, float b, float c);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:neuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSNNNeuronDescriptor neuronDescriptor);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnConvolutionDescriptor" /> that describes subpixel upsampling and reshaping.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnConvolutionDescriptor), Name = "MPSCNNSubPixelConvolutionDescriptor")]
	[DisableDefaultCtor]
	interface MPSCnnSubPixelConvolutionDescriptor {
		[Export ("subPixelScaleFactor")]
		nuint SubPixelScaleFactor { get; set; }
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnConvolutionDescriptor" /> that describes depthwise convolution.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnConvolutionDescriptor), Name = "MPSCNNDepthWiseConvolutionDescriptor")]
	[DisableDefaultCtor]
	interface MPSCnnDepthWiseConvolutionDescriptor {
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("channelMultiplier")]
		nuint ChannelMultiplier { get; }
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnKernel" /> that transposes its input.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel), Name = "MPSCNNConvolutionTranspose")]
	[DisableDefaultCtor] // There is a DesignatedInitializer, file a bug if needed.
	interface MPSCnnConvolutionTranspose {
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("inputFeatureChannels")]
		nuint InputFeatureChannels { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("outputFeatureChannels")]
		nuint OutputFeatureChannels { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("kernelOffsetX")]
		nint KernelOffsetX { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("kernelOffsetY")]
		nint KernelOffsetY { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("groups")]
		nuint Groups { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("accumulatorPrecisionOption", ArgumentSemantic.Assign)]
		MPSNNConvolutionAccumulatorPrecisionOption AccumulatorPrecisionOption { get; set; }

		[Export ("initWithDevice:weights:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, IMPSCnnConvolutionDataSource weights);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[MacCatalyst (13, 1)]
		[Export ("encodeToCommandBuffer:sourceImage:convolutionGradientState:")]
		MPSImage EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSImage sourceImage, [NullAllowed] MPSCnnConvolutionGradientState convolutionGradientState);

		[MacCatalyst (13, 1)]
		[Export ("encodeBatchToCommandBuffer:sourceImages:convolutionGradientStates:")]
		NSArray<MPSImage> EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceImage, [NullAllowed] MPSCnnConvolutionGradientState [] convolutionGradientState);

		[MacCatalyst (13, 1)]
		[Export ("encodeToCommandBuffer:sourceImage:convolutionGradientState:destinationImage:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSImage sourceImage, [NullAllowed] MPSCnnConvolutionGradientState convolutionGradientState, MPSImage destinationImage);

		[MacCatalyst (13, 1)]
		[Export ("encodeBatchToCommandBuffer:sourceImages:convolutionGradientStates:destinationImages:")]
		void EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceImage, [NullAllowed] MPSCnnConvolutionGradientState [] convolutionGradientState, NSArray<MPSImage> destinationImage);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnKernel" /> that has binary weights and convolves its input.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel), Name = "MPSCNNBinaryConvolution")]
	[DisableDefaultCtor]
	interface MPSCnnBinaryConvolution {
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("inputFeatureChannels")]
		nuint InputFeatureChannels { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("outputFeatureChannels")]
		nuint OutputFeatureChannels { get; }

		[Export ("initWithDevice:convolutionData:scaleValue:type:flags:")]
		NativeHandle Constructor (IMTLDevice device, IMPSCnnConvolutionDataSource convolutionData, float scaleValue, MPSCnnBinaryConvolutionType type, MPSCnnBinaryConvolutionFlags flags);

		[Internal, Sealed, Export ("initWithDevice:convolutionData:outputBiasTerms:outputScaleTerms:inputBiasTerms:inputScaleTerms:type:flags:")]
		IntPtr InitWithDevice (IMTLDevice device, IMPSCnnConvolutionDataSource convolutionData, [NullAllowed] IntPtr /* float* */ outputBiasTerms, [NullAllowed] IntPtr /* float* */ outputScaleTerms, [NullAllowed] IntPtr /* float* */ inputBiasTerms, [NullAllowed] IntPtr /* float* */ inputScaleTerms, MPSCnnBinaryConvolutionType type, MPSCnnBinaryConvolutionFlags flags);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnBinaryConvolution" /> that is a fully-connected convolution layer that uses binary weights.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnBinaryConvolution), Name = "MPSCNNBinaryFullyConnected")]
	[DisableDefaultCtor]
	interface MPSCnnBinaryFullyConnected {
		[Export ("initWithDevice:convolutionData:scaleValue:type:flags:")]
		NativeHandle Constructor (IMTLDevice device, IMPSCnnConvolutionDataSource convolutionData, float scaleValue, MPSCnnBinaryConvolutionType type, MPSCnnBinaryConvolutionFlags flags);

		[Internal, Sealed, Export ("initWithDevice:convolutionData:outputBiasTerms:outputScaleTerms:inputBiasTerms:inputScaleTerms:type:flags:")]
		IntPtr InitWithDevice (IMTLDevice device, IMPSCnnConvolutionDataSource convolutionData, [NullAllowed] IntPtr /* float* */ outputBiasTerms, [NullAllowed] IntPtr /* float* */ outputScaleTerms, [NullAllowed] IntPtr /* float* */ inputBiasTerms, [NullAllowed] IntPtr /* float* */ inputScaleTerms, MPSCnnBinaryConvolutionType type, MPSCnnBinaryConvolutionFlags flags);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>An L2-norm pooling filter.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnPooling), Name = "MPSCNNPoolingL2Norm")]
	[DisableDefaultCtor] // failed assertion.
	interface MPSCnnPoolingL2Norm {
		[Export ("initWithDevice:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>A dilated max <see cref="T:MetalPerformanceShaders.MPSCnnPooling" /> filter.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnPooling), Name = "MPSCNNDilatedPoolingMax")]
	[DisableDefaultCtor] // failed assertion.
	interface MPSCnnDilatedPoolingMax {
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("dilationRateX")]
		nuint DilationRateX { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("dilationRateY")]
		nuint DilationRateY { get; }

		[Export ("initWithDevice:kernelWidth:kernelHeight:dilationRateX:dilationRateY:strideInPixelsX:strideInPixelsY:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight, nuint dilationRateX, nuint dilationRateY, nuint strideInPixelsX, nuint strideInPixelsY);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnGradientKernel), Name = "MPSCNNPoolingGradient")]
	[DisableDefaultCtor]
	interface MPSCnnPoolingGradient {

		[Export ("sourceSize", ArgumentSemantic.Assign)]
		MTLSize SourceSize { get; set; }

		[Export ("initWithDevice:kernelWidth:kernelHeight:")]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight);

		[Export ("initWithDevice:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnPoolingGradient), Name = "MPSCNNPoolingAverageGradient")]
	[DisableDefaultCtor]
	interface MPSCnnPoolingAverageGradient {

		[Export ("zeroPadSizeX")]
		nuint ZeroPadSizeX { get; set; }

		[Export ("zeroPadSizeY")]
		nuint ZeroPadSizeY { get; set; }

		[Export ("initWithDevice:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnPoolingGradient), Name = "MPSCNNPoolingMaxGradient")]
	[DisableDefaultCtor]
	interface MPSCnnPoolingMaxGradient {

		[Export ("initWithDevice:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnPoolingGradient), Name = "MPSCNNPoolingL2NormGradient")]
	[DisableDefaultCtor]
	interface MPSCnnPoolingL2NormGradient {

		[Export ("initWithDevice:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnPoolingGradient), Name = "MPSCNNDilatedPoolingMaxGradient")]
	[DisableDefaultCtor]
	interface MPSCnnDilatedPoolingMaxGradient {

		[Export ("initWithDevice:kernelWidth:kernelHeight:dilationRateX:dilationRateY:strideInPixelsX:strideInPixelsY:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelWidth, nuint kernelHeight, nuint dilationRateX, nuint dilationRateY, nuint strideInPixelsX, nuint strideInPixelsY);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnKernel" /> that upsamples an image.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel), Name = "MPSCNNUpsampling")]
	[DisableDefaultCtor] // failed assertion
	interface MPSCnnUpsampling {
		[Export ("scaleFactorX")]
		double ScaleFactorX { get; }

		[Export ("scaleFactorY")]
		double ScaleFactorY { get; }

		[MacCatalyst (13, 1)]
		[Export ("alignCorners")]
		bool AlignCorners { get; }

		// inlining ctor from base class
		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnUpsampling" /> filter that performs nearest spatial upsampling.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnUpsampling), Name = "MPSCNNUpsamplingNearest")]
	[DisableDefaultCtor] // failed assertion.
	interface MPSCnnUpsamplingNearest {
		[Export ("initWithDevice:integerScaleFactorX:integerScaleFactorY:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint integerScaleFactorX, nuint integerScaleFactorY);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnUpsampling" /> filter that performs bilinear spatial upsampling.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnUpsampling), Name = "MPSCNNUpsamplingBilinear")]
	[DisableDefaultCtor] // failed assertion.
	interface MPSCnnUpsamplingBilinear {
		[Export ("initWithDevice:integerScaleFactorX:integerScaleFactorY:")]
		NativeHandle Constructor (IMTLDevice device, nuint integerScaleFactorX, nuint integerScaleFactorY);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:integerScaleFactorX:integerScaleFactorY:alignCorners:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint integerScaleFactorX, nuint integerScaleFactorY, bool alignCorners);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnGradientKernel), Name = "MPSCNNUpsamplingGradient")]
	[DisableDefaultCtor]
	interface MPSCnnUpsamplingGradient {

		[Export ("scaleFactorX")]
		double ScaleFactorX { get; }

		[Export ("scaleFactorY")]
		double ScaleFactorY { get; }

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnUpsamplingGradient), Name = "MPSCNNUpsamplingNearestGradient")]
	[DisableDefaultCtor]
	interface MPSCnnUpsamplingNearestGradient {

		[Export ("initWithDevice:integerScaleFactorX:integerScaleFactorY:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint integerScaleFactorX, nuint integerScaleFactorY);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnUpsamplingGradient), Name = "MPSCNNUpsamplingBilinearGradient")]
	[DisableDefaultCtor]
	interface MPSCnnUpsamplingBilinearGradient {

		[Export ("initWithDevice:integerScaleFactorX:integerScaleFactorY:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint integerScaleFactorX, nuint integerScaleFactorY);
	}

	/// <summary>A structural description of a recurrent neural net layer.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject), Name = "MPSRNNDescriptor")]
	interface MPSRnnDescriptor {
		[Export ("inputFeatureChannels")]
		nuint InputFeatureChannels { get; set; }

		[Export ("outputFeatureChannels")]
		nuint OutputFeatureChannels { get; set; }

		[Export ("useLayerInputUnitTransformMode")]
		bool UseLayerInputUnitTransformMode { get; set; }

		[Export ("useFloat32Weights")]
		bool UseFloat32Weights { get; set; }

		[Export ("layerSequenceDirection", ArgumentSemantic.Assign)]
		MPSRnnSequenceDirection LayerSequenceDirection { get; set; }
	}

	/// <summary>Describes the internal gate in a recurrent neural net.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSRnnDescriptor), Name = "MPSRNNSingleGateDescriptor")]
	interface MPSRnnSingleGateDescriptor {
		[NullAllowed, Export ("inputWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource InputWeights { get; set; }

		[NullAllowed, Export ("recurrentWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource RecurrentWeights { get; set; }

		[Static]
		[Export ("createRNNSingleGateDescriptorWithInputFeatureChannels:outputFeatureChannels:")]
		MPSRnnSingleGateDescriptor Create (nuint inputFeatureChannels, nuint outputFeatureChannels);
	}

	/// <summary>Describes a gated recurrent unit layer in a neural net.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSRnnDescriptor))]
	interface MPSGRUDescriptor {
		[NullAllowed, Export ("inputGateInputWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource InputGateInputWeights { get; set; }

		[NullAllowed, Export ("inputGateRecurrentWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource InputGateRecurrentWeights { get; set; }

		[NullAllowed, Export ("recurrentGateInputWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource RecurrentGateInputWeights { get; set; }

		[NullAllowed, Export ("recurrentGateRecurrentWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource RecurrentGateRecurrentWeights { get; set; }

		[NullAllowed, Export ("outputGateInputWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource OutputGateInputWeights { get; set; }

		[NullAllowed, Export ("outputGateRecurrentWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource OutputGateRecurrentWeights { get; set; }

		[NullAllowed, Export ("outputGateInputGateWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource OutputGateInputGateWeights { get; set; }

		[Export ("gatePnormValue")]
		float GatePnormValue { get; set; }

		[Export ("flipOutputGates")]
		bool FlipOutputGates { get; set; }

		[Static]
		[Export ("createGRUDescriptorWithInputFeatureChannels:outputFeatureChannels:")]
		MPSGRUDescriptor Create (nuint inputFeatureChannels, nuint outputFeatureChannels);
	}

	/// <summary>Describes a Long-Short Term Memory layer in neural net.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSRnnDescriptor))]
	interface MPSLSTMDescriptor {
		[Export ("memoryWeightsAreDiagonal")]
		bool AreMemoryWeightsDiagonal { get; set; }

		[NullAllowed, Export ("inputGateInputWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource InputGateInputWeights { get; set; }

		[NullAllowed, Export ("inputGateRecurrentWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource InputGateRecurrentWeights { get; set; }

		[NullAllowed, Export ("inputGateMemoryWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource InputGateMemoryWeights { get; set; }

		[NullAllowed, Export ("forgetGateInputWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource ForgetGateInputWeights { get; set; }

		[NullAllowed, Export ("forgetGateRecurrentWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource ForgetGateRecurrentWeights { get; set; }

		[NullAllowed, Export ("forgetGateMemoryWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource ForgetGateMemoryWeights { get; set; }

		[NullAllowed, Export ("outputGateInputWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource OutputGateInputWeights { get; set; }

		[NullAllowed, Export ("outputGateRecurrentWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource OutputGateRecurrentWeights { get; set; }

		[NullAllowed, Export ("outputGateMemoryWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource OutputGateMemoryWeights { get; set; }

		[NullAllowed, Export ("cellGateInputWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource CellGateInputWeights { get; set; }

		[NullAllowed, Export ("cellGateRecurrentWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource CellGateRecurrentWeights { get; set; }

		[NullAllowed, Export ("cellGateMemoryWeights", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource CellGateMemoryWeights { get; set; }

		[Export ("cellToOutputNeuronType", ArgumentSemantic.Assign)]
		MPSCnnNeuronType CellToOutputNeuronType { get; set; }

		[Export ("cellToOutputNeuronParamA")]
		float CellToOutputNeuronParamA { get; set; }

		[Export ("cellToOutputNeuronParamB")]
		float CellToOutputNeuronParamB { get; set; }

		[MacCatalyst (13, 1)]
		[Export ("cellToOutputNeuronParamC")]
		float CellToOutputNeuronParamC { get; set; }

		[Static]
		[Export ("createLSTMDescriptorWithInputFeatureChannels:outputFeatureChannels:")]
		MPSLSTMDescriptor Create (nuint inputFeatureChannels, nuint outputFeatureChannels);
	}

	/// <summary>The image containing the state in an image-baed recurrent neural net as it iterates through its sequence.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSState), Name = "MPSRNNRecurrentImageState")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSRnnRecurrentImageState {
		[Export ("getRecurrentOutputImageForLayerIndex:")]
		[return: NullAllowed]
		MPSImage GetRecurrentOutputImage (nuint layerIndex);

		[Export ("getMemoryCellImageForLayerIndex:")]
		[return: NullAllowed]
		MPSImage GetMemoryCellImage (nuint layerIndex);
	}

	/// <summary>A recurrent neural net layer specifically for image data.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel), Name = "MPSRNNImageInferenceLayer")]
	[DisableDefaultCtor] // There is a DesignatedInitializer, file a bug if needed.
	interface MPSRnnImageInferenceLayer {
		[Export ("inputFeatureChannels")]
		nuint InputFeatureChannels { get; }

		[Export ("outputFeatureChannels")]
		nuint OutputFeatureChannels { get; }

		[Export ("numberOfLayers")]
		nuint NumberOfLayers { get; }

		[Export ("recurrentOutputIsTemporary")]
		bool IsRecurrentOutputTemporary { get; set; }

		[Export ("storeAllIntermediateStates")]
		bool StoreAllIntermediateStates { get; set; }

		[Export ("bidirectionalCombineMode", ArgumentSemantic.Assign)]
		MPSRnnBidirectionalCombineMode BidirectionalCombineMode { get; set; }

		[Export ("initWithDevice:rnnDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSRnnDescriptor rnnDescriptor);

		[Export ("initWithDevice:rnnDescriptors:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSRnnDescriptor [] rnnDescriptors);

		[Export ("encodeSequenceToCommandBuffer:sourceImages:destinationImages:recurrentInputState:recurrentOutputStates:")]
		void EncodeSequence (IMTLCommandBuffer commandBuffer, MPSImage [] sourceImages, MPSImage [] destinationImages, [NullAllowed] MPSRnnRecurrentImageState recurrentInputState, [NullAllowed] NSMutableArray<MPSRnnRecurrentImageState> recurrentOutputStates);

		[Export ("encodeBidirectionalSequenceToCommandBuffer:sourceSequence:destinationForwardImages:destinationBackwardImages:")]
		void EncodeBidirectionalSequence (IMTLCommandBuffer commandBuffer, MPSImage [] sourceSequence, MPSImage [] destinationForwardImages, [NullAllowed] MPSImage [] destinationBackwardImages);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("copyWithZone:device:")]
		[return: Release ()]
		MPSRnnImageInferenceLayer Copy ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);
	}

	/// <summary>The matrix containing the state as a recurrent neural net iterates through its sequence.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSState), Name = "MPSRNNRecurrentMatrixState")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSRnnRecurrentMatrixState {
		[Export ("getRecurrentOutputMatrixForLayerIndex:")]
		[return: NullAllowed]
		MPSMatrix GetRecurrentOutputMatrix (nuint layerIndex);

		[Export ("getMemoryCellMatrixForLayerIndex:")]
		[return: NullAllowed]
		MPSMatrix GetMemoryCellMatrix (nuint layerIndex);
	}

	/// <summary>A recurrent neural net layer.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel), Name = "MPSRNNMatrixInferenceLayer")]
	[DisableDefaultCtor] // There is a DesignatedInitializer, file a bug if needed.
	interface MPSRnnMatrixInferenceLayer {
		[Export ("inputFeatureChannels")]
		nuint InputFeatureChannels { get; }

		[Export ("outputFeatureChannels")]
		nuint OutputFeatureChannels { get; }

		[Export ("numberOfLayers")]
		nuint NumberOfLayers { get; }

		[Export ("recurrentOutputIsTemporary")]
		bool IsRecurrentOutputTemporary { get; set; }

		[Export ("storeAllIntermediateStates")]
		bool StoreAllIntermediateStates { get; set; }

		[Export ("bidirectionalCombineMode", ArgumentSemantic.Assign)]
		MPSRnnBidirectionalCombineMode BidirectionalCombineMode { get; set; }

		[Export ("initWithDevice:rnnDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSRnnDescriptor rnnDescriptor);

		[Export ("initWithDevice:rnnDescriptors:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSRnnDescriptor [] rnnDescriptors);

		[Export ("encodeSequenceToCommandBuffer:sourceMatrices:destinationMatrices:recurrentInputState:recurrentOutputStates:")]
		void EncodeSequence (IMTLCommandBuffer commandBuffer, MPSMatrix [] sourceMatrices, MPSMatrix [] destinationMatrices, [NullAllowed] MPSRnnRecurrentMatrixState recurrentInputState, [NullAllowed] NSMutableArray<MPSRnnRecurrentMatrixState> recurrentOutputStates);

		[MacCatalyst (13, 1)]
		[Export ("encodeSequenceToCommandBuffer:sourceMatrices:sourceOffsets:destinationMatrices:destinationOffsets:recurrentInputState:recurrentOutputStates:")]
		void EncodeSequence (IMTLCommandBuffer commandBuffer, MPSMatrix [] sourceMatrices, [NullAllowed] IntPtr sourceOffsets, MPSMatrix [] destinationMatrices, [NullAllowed] IntPtr destinationOffsets, [NullAllowed] MPSRnnRecurrentMatrixState recurrentInputState, [NullAllowed] NSMutableArray<MPSRnnRecurrentMatrixState> recurrentOutputStates);

		[Export ("encodeBidirectionalSequenceToCommandBuffer:sourceSequence:destinationForwardMatrices:destinationBackwardMatrices:")]
		void EncodeBidirectionalSequence (IMTLCommandBuffer commandBuffer, MPSMatrix [] sourceSequence, MPSMatrix [] destinationForwardMatrices, [NullAllowed] MPSMatrix [] destinationBackwardMatrices);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("copyWithZone:device:")]
		[return: Release ()]
		MPSRnnMatrixInferenceLayer Copy ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);
	}

	/// <summary>A placeholder node for an image in a neural network graph.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MPSNNImageNode {
		[Export ("initWithHandle:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] IMPSHandle handle);

		[Static]
		[Export ("nodeWithHandle:")]
		MPSNNImageNode Create ([NullAllowed] IMPSHandle handle);

		[Static]
		[Export ("exportedNodeWithHandle:")]
		MPSNNImageNode GetExportedNode ([NullAllowed] IMPSHandle handle);

		[NullAllowed, Export ("handle", ArgumentSemantic.Retain)]
		IMPSHandle MPSHandle { get; set; }

		[Export ("format", ArgumentSemantic.Assign)]
		MPSImageFeatureChannelFormat Format { get; set; }

		[Export ("imageAllocator", ArgumentSemantic.Retain)]
		IMPSImageAllocator ImageAllocator { get; set; }

		[Export ("exportFromGraph")]
		bool ExportFromGraph { get; set; }

		[MacCatalyst (13, 1)]
		[Export ("synchronizeResource")]
		bool SynchronizeResource { get; set; }

		[MacCatalyst (13, 1)]
		[Export ("stopGradient")]
		bool StopGradient { get; set; }
	}

	/// <summary>A state object in a neural network graph.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MPSNNStateNode {
		[NullAllowed, Export ("handle", ArgumentSemantic.Retain)]
		IMPSHandle MPSHandle { get; set; }

		[Export ("exportFromGraph")]
		bool ExportFromGraph { get; set; }

		[MacCatalyst (13, 1)]
		[Export ("synchronizeResource")]
		bool SynchronizeResource { get; set; }
	}

	[MacCatalyst (13, 1)]
	delegate void MPSGradientNodeHandler (MPSNNFilterNode gradientNode, MPSNNFilterNode inferenceNode, MPSNNImageNode inferenceSource, MPSNNImageNode gradientSource);

	/// <summary>A placeholder node in a neural network graph for an image filtering stage.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MPSNNFilterNode {
		[Export ("resultImage")]
		MPSNNImageNode ResultImage { get; }

		[NullAllowed, Export ("resultState")]
		MPSNNStateNode ResultState { get; }

		[NullAllowed, Export ("resultStates")]
		MPSNNStateNode [] ResultStates { get; }

		[Export ("paddingPolicy", ArgumentSemantic.Retain)]
		IMPSNNPadding PaddingPolicy { get; set; }

		[NullAllowed, Export ("label")]
		string Label { get; set; }

		[MacCatalyst (13, 1)]
		[Export ("gradientFilterWithSource:")]
		MPSNNGradientFilterNode GetFilter (MPSNNImageNode gradientImageSource);

		[MacCatalyst (13, 1)]
		[Export ("gradientFilterWithSources:")]
		MPSNNGradientFilterNode GetFilter (MPSNNImageNode [] gradientImagesSources);

		[MacCatalyst (13, 1)]
		[Export ("gradientFiltersWithSources:")]
		MPSNNGradientFilterNode [] GetFilters (MPSNNImageNode [] gradientImagesSources);

		[MacCatalyst (13, 1)]
		[Export ("gradientFiltersWithSource:")]
		MPSNNGradientFilterNode [] GetFilters (MPSNNImageNode gradientImageSource);

		[MacCatalyst (13, 1)]
		[Export ("trainingGraphWithSourceGradient:nodeHandler:")]
		[return: NullAllowed]
		MPSNNFilterNode [] GetTrainingGraph ([NullAllowed] MPSNNImageNode gradientImageSource, [NullAllowed] MPSGradientNodeHandler nodeHandler);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode))]
	[DisableDefaultCtor]
	interface MPSNNGradientFilterNode {
	}

	/// <summary>Subclass of <see cref="T:MetalPerformanceShaders.MPSNNFilterNode" /> that is the base class for convolution representation nodes.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode), Name = "MPSCNNConvolutionNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnConvolutionNode {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("accumulatorPrecision", ArgumentSemantic.Assign)]
		MPSNNConvolutionAccumulatorPrecisionOption AccumulatorPrecision { get; set; }

		[Static]
		[Export ("nodeWithSource:weights:")]
		MPSCnnConvolutionNode Create (MPSNNImageNode sourceNode, IMPSCnnConvolutionDataSource weights);

		[Export ("initWithSource:weights:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, IMPSCnnConvolutionDataSource weights);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[NullAllowed, Export ("convolutionGradientState")]
		MPSCnnConvolutionGradientStateNode ConvolutionGradientState { get; }
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnConvolutionNode" /> that represents a fully connected layer.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnConvolutionNode), Name = "MPSCNNFullyConnectedNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnFullyConnectedNode {
		[Static]
		[Export ("nodeWithSource:weights:")]
		MPSCnnFullyConnectedNode Create (MPSNNImageNode sourceNode, IMPSCnnConvolutionDataSource weights);

		[Export ("initWithSource:weights:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, IMPSCnnConvolutionDataSource weights);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnConvolutionNode" /> that represents a binary convolution kernel.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnConvolutionNode), Name = "MPSCNNBinaryConvolutionNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnBinaryConvolutionNode {
		[Static]
		[Export ("nodeWithSource:weights:scaleValue:type:flags:")]
		MPSCnnBinaryConvolutionNode Create (MPSNNImageNode sourceNode, IMPSCnnConvolutionDataSource weights, float scaleValue, MPSCnnBinaryConvolutionType type, MPSCnnBinaryConvolutionFlags flags);

		[Export ("initWithSource:weights:scaleValue:type:flags:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, IMPSCnnConvolutionDataSource weights, float scaleValue, MPSCnnBinaryConvolutionType type, MPSCnnBinaryConvolutionFlags flags);

		[Internal]
		[MacCatalyst (13, 1)]
		[Static]
		[Export ("nodeWithSource:weights:outputBiasTerms:outputScaleTerms:inputBiasTerms:inputScaleTerms:type:flags:")]
		MPSCnnBinaryConvolutionNode Create (MPSNNImageNode sourceNode, IMPSCnnConvolutionDataSource weights, [NullAllowed] IntPtr outputBiasTerms, [NullAllowed] IntPtr outputScaleTerms, [NullAllowed] IntPtr inputBiasTerms, [NullAllowed] IntPtr inputScaleTerms, MPSCnnBinaryConvolutionType type, MPSCnnBinaryConvolutionFlags flags);

		[Internal]
		[MacCatalyst (13, 1)]
		[Export ("initWithSource:weights:outputBiasTerms:outputScaleTerms:inputBiasTerms:inputScaleTerms:type:flags:")]
		IntPtr InitWithSource (MPSNNImageNode sourceNode, IMPSCnnConvolutionDataSource weights, [NullAllowed] IntPtr outputBiasTerms, [NullAllowed] IntPtr outputScaleTerms, [NullAllowed] IntPtr inputBiasTerms, [NullAllowed] IntPtr inputScaleTerms, MPSCnnBinaryConvolutionType type, MPSCnnBinaryConvolutionFlags flags);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnBinaryConvolutionNode" /> that represents a fully-connected convolution layer that uses binary weights.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnBinaryConvolutionNode), Name = "MPSCNNBinaryFullyConnectedNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnBinaryFullyConnectedNode {
		[Static]
		[Export ("nodeWithSource:weights:scaleValue:type:flags:")]
		MPSCnnBinaryFullyConnectedNode Create (MPSNNImageNode sourceNode, IMPSCnnConvolutionDataSource weights, float scaleValue, MPSCnnBinaryConvolutionType type, MPSCnnBinaryConvolutionFlags flags);

		[Export ("initWithSource:weights:scaleValue:type:flags:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, IMPSCnnConvolutionDataSource weights, float scaleValue, MPSCnnBinaryConvolutionType type, MPSCnnBinaryConvolutionFlags flags);

		[Internal]
		[MacCatalyst (13, 1)]
		[Static]
		[Export ("nodeWithSource:weights:outputBiasTerms:outputScaleTerms:inputBiasTerms:inputScaleTerms:type:flags:")]
		MPSCnnBinaryFullyConnectedNode Create (MPSNNImageNode sourceNode, IMPSCnnConvolutionDataSource weights, [NullAllowed] IntPtr outputBiasTerms, [NullAllowed] IntPtr outputScaleTerms, [NullAllowed] IntPtr inputBiasTerms, [NullAllowed] IntPtr inputScaleTerms, MPSCnnBinaryConvolutionType type, MPSCnnBinaryConvolutionFlags flags);

		[Internal]
		[MacCatalyst (13, 1)]
		[Export ("initWithSource:weights:outputBiasTerms:outputScaleTerms:inputBiasTerms:inputScaleTerms:type:flags:")]
		IntPtr InitWithSource (MPSNNImageNode sourceNode, IMPSCnnConvolutionDataSource weights, [NullAllowed] IntPtr outputBiasTerms, [NullAllowed] IntPtr outputScaleTerms, [NullAllowed] IntPtr inputBiasTerms, [NullAllowed] IntPtr inputScaleTerms, MPSCnnBinaryConvolutionType type, MPSCnnBinaryConvolutionFlags flags);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnConvolutionNode" /> that represents a transpose kernel.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnConvolutionNode), Name = "MPSCNNConvolutionTransposeNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnConvolutionTransposeNode {
		[MacCatalyst (13, 1)]
		[Static]
		[Export ("nodeWithSource:convolutionGradientState:weights:")]
		MPSCnnConvolutionTransposeNode Create (MPSNNImageNode sourceNode, [NullAllowed] MPSCnnConvolutionGradientStateNode convolutionGradientState, IMPSCnnConvolutionDataSource weights);

		[MacCatalyst (13, 1)]
		[Export ("initWithSource:convolutionGradientState:weights:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, [NullAllowed] MPSCnnConvolutionGradientStateNode convolutionGradientState, IMPSCnnConvolutionDataSource weights);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientFilterNode), Name = "MPSCNNConvolutionGradientNode")]
	[DisableDefaultCtor]
	interface MPSCnnConvolutionGradientNode : MPSNNTrainableNode {

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:convolutionGradientState:weights:")]
		MPSCnnConvolutionGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSCnnConvolutionGradientStateNode gradientState, [NullAllowed] IMPSCnnConvolutionDataSource weights);

		[Export ("initWithSourceGradient:sourceImage:convolutionGradientState:weights:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSCnnConvolutionGradientStateNode gradientState, [NullAllowed] IMPSCnnConvolutionDataSource weights);
	}

	/// <summary>The base class for representations of activation functions.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode), Name = "MPSCNNNeuronNode")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronNode {

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("nodeWithSource:descriptor:")]
		MPSCnnNeuronNode Create (MPSNNImageNode sourceNode, MPSNNNeuronDescriptor descriptor);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("a")]
		float A { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("b")]
		float B { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("c")]
		float C { get; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuronNode), Name = "MPSCNNNeuronPowerNode")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronPowerNode {

		[Static]
		[Export ("nodeWithSource:a:b:c:")]
		MPSCnnNeuronPowerNode Create (MPSNNImageNode sourceNode, float a, float b, float c);

		[Export ("initWithSource:a:b:c:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, float a, float b, float c);

		[Static]
		[Export ("nodeWithSource:")]
		MPSCnnNeuronPowerNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuronNode), Name = "MPSCNNNeuronExponentialNode")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronExponentialNode {

		[Static]
		[Export ("nodeWithSource:a:b:c:")]
		MPSCnnNeuronExponentialNode Create (MPSNNImageNode sourceNode, float a, float b, float c);

		[Export ("initWithSource:a:b:c:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, float a, float b, float c);

		[Static]
		[Export ("nodeWithSource:")]
		MPSCnnNeuronExponentialNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuronNode), Name = "MPSCNNNeuronLogarithmNode")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronLogarithmNode {

		[Static]
		[Export ("nodeWithSource:a:b:c:")]
		MPSCnnNeuronLogarithmNode Create (MPSNNImageNode sourceNode, float a, float b, float c);

		[Export ("initWithSource:a:b:c:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, float a, float b, float c);

		[Static]
		[Export ("nodeWithSource:")]
		MPSCnnNeuronLogarithmNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientFilterNode), Name = "MPSCNNNeuronGradientNode")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronGradientNode {

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:descriptor:")]
		MPSCnnNeuronGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, MPSNNNeuronDescriptor descriptor);

		[Export ("initWithSourceGradient:sourceImage:gradientState:descriptor:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, MPSNNNeuronDescriptor descriptor);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("descriptor")]
		MPSNNNeuronDescriptor Descriptor { get; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode))]
	[DisableDefaultCtor]
	interface MPSNNUnaryReductionNode {

		[Export ("clipRectSource", ArgumentSemantic.Assign)]
		MTLRegion ClipRectSource { get; set; }

		[Static]
		[Export ("nodeWithSource:")]
		MPSNNUnaryReductionNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNUnaryReductionNode))]
	[DisableDefaultCtor]
	interface MPSNNReductionRowMinNode {

		[Static]
		[New]
		[Export ("nodeWithSource:")]
		MPSNNReductionRowMinNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNUnaryReductionNode))]
	[DisableDefaultCtor]
	interface MPSNNReductionColumnMinNode {

		[Static]
		[New]
		[Export ("nodeWithSource:")]
		MPSNNReductionColumnMinNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNUnaryReductionNode))]
	[DisableDefaultCtor]
	interface MPSNNReductionFeatureChannelsMinNode {

		[Static]
		[New]
		[Export ("nodeWithSource:")]
		MPSNNReductionFeatureChannelsMinNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNUnaryReductionNode))]
	[DisableDefaultCtor]
	interface MPSNNReductionFeatureChannelsArgumentMinNode {

		[Static]
		[New]
		[Export ("nodeWithSource:")]
		MPSNNReductionFeatureChannelsArgumentMinNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNUnaryReductionNode))]
	[DisableDefaultCtor]
	interface MPSNNReductionRowMaxNode {

		[Static]
		[New]
		[Export ("nodeWithSource:")]
		MPSNNReductionRowMaxNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNUnaryReductionNode))]
	[DisableDefaultCtor]
	interface MPSNNReductionColumnMaxNode {

		[Static]
		[New]
		[Export ("nodeWithSource:")]
		MPSNNReductionColumnMaxNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNUnaryReductionNode))]
	[DisableDefaultCtor]
	interface MPSNNReductionFeatureChannelsMaxNode {

		[Static]
		[New]
		[Export ("nodeWithSource:")]
		MPSNNReductionFeatureChannelsMaxNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNUnaryReductionNode))]
	[DisableDefaultCtor]
	interface MPSNNReductionFeatureChannelsArgumentMaxNode {

		[Static]
		[New]
		[Export ("nodeWithSource:")]
		MPSNNReductionFeatureChannelsArgumentMaxNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNUnaryReductionNode))]
	[DisableDefaultCtor]
	interface MPSNNReductionRowMeanNode {

		[Static]
		[New]
		[Export ("nodeWithSource:")]
		MPSNNReductionRowMeanNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNUnaryReductionNode))]
	[DisableDefaultCtor]
	interface MPSNNReductionColumnMeanNode {

		[Static]
		[New]
		[Export ("nodeWithSource:")]
		MPSNNReductionColumnMeanNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNUnaryReductionNode))]
	[DisableDefaultCtor]
	interface MPSNNReductionFeatureChannelsMeanNode {

		[Static]
		[New]
		[Export ("nodeWithSource:")]
		MPSNNReductionFeatureChannelsMeanNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNUnaryReductionNode))]
	[DisableDefaultCtor]
	interface MPSNNReductionSpatialMeanNode {

		[Static]
		[New]
		[Export ("nodeWithSource:")]
		MPSNNReductionSpatialMeanNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNUnaryReductionNode))]
	[DisableDefaultCtor]
	interface MPSNNReductionRowSumNode {

		[Static]
		[New]
		[Export ("nodeWithSource:")]
		MPSNNReductionRowSumNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNUnaryReductionNode))]
	[DisableDefaultCtor]
	interface MPSNNReductionColumnSumNode {

		[Static]
		[New]
		[Export ("nodeWithSource:")]
		MPSNNReductionColumnSumNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNUnaryReductionNode))]
	[DisableDefaultCtor]
	interface MPSNNReductionFeatureChannelsSumNode {

		[Static]
		[New]
		[Export ("nodeWithSource:")]
		MPSNNReductionFeatureChannelsSumNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);

		[Export ("weight")]
		float Weight { get; set; }
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnNeuronNode" /> that represents the absolute-value activation function.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuronNode), Name = "MPSCNNNeuronAbsoluteNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnNeuronAbsoluteNode {
		[Static]
		[Export ("nodeWithSource:")]
		MPSCnnNeuronAbsoluteNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnNeuronNode" /> that represents the exponential linear unit activation function.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuronNode), Name = "MPSCNNNeuronELUNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnNeuronEluNode {
		[Static]
		[Export ("nodeWithSource:a:")]
		MPSCnnNeuronEluNode Create (MPSNNImageNode sourceNode, float a);

		[Static]
		[Export ("nodeWithSource:")]
		MPSCnnNeuronEluNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);

		[Export ("initWithSource:a:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, float a);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnNeuronNode" /> that represents the ReLUN activation function.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuronNode), Name = "MPSCNNNeuronReLUNNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnNeuronReLunNode {
		[Static]
		[Export ("nodeWithSource:a:b:")]
		MPSCnnNeuronReLunNode Create (MPSNNImageNode sourceNode, float a, float b);

		[Export ("initWithSource:a:b:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, float a, float b);

		[Static]
		[Export ("nodeWithSource:")]
		MPSCnnNeuronReLunNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnNeuronNode" /> that represents the linear activation function.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuronNode), Name = "MPSCNNNeuronLinearNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnNeuronLinearNode {
		[Static]
		[Export ("nodeWithSource:a:b:")]
		MPSCnnNeuronLinearNode Create (MPSNNImageNode sourceNode, float a, float b);

		[Deprecated (PlatformName.TvOS, 12, 0)]
		[Deprecated (PlatformName.iOS, 12, 0)]
		[Deprecated (PlatformName.MacOSX, 10, 14)]
		[Deprecated (PlatformName.MacCatalyst, 13, 1)]
		[Export ("initWithSource:a:b:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, float a, float b);

		[Static]
		[Export ("nodeWithSource:")]
		MPSCnnNeuronLinearNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnNeuronNode" /> that represents the rectified linear unit activation function.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuronNode), Name = "MPSCNNNeuronReLUNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnNeuronReLUNode {
		[Static]
		[Export ("nodeWithSource:a:")]
		MPSCnnNeuronReLUNode Create (MPSNNImageNode sourceNode, float a);

		[Static]
		[Export ("nodeWithSource:")]
		MPSCnnNeuronReLUNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);

		[Export ("initWithSource:a:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, float a);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnNeuronNode" /> that represents the sigmoid activation function.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuronNode), Name = "MPSCNNNeuronSigmoidNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnNeuronSigmoidNode {
		[Static]
		[Export ("nodeWithSource:")]
		MPSCnnNeuronSigmoidNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnNeuronNode" /> that represents the hard sigmoid activation function.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuronNode), Name = "MPSCNNNeuronHardSigmoidNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnNeuronHardSigmoidNode {
		[Static]
		[Export ("nodeWithSource:a:b:")]
		MPSCnnNeuronHardSigmoidNode Create (MPSNNImageNode sourceNode, float a, float b);

		[Export ("initWithSource:a:b:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, float a, float b);

		[Static]
		[Export ("nodeWithSource:")]
		MPSCnnNeuronHardSigmoidNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnNeuronNode" /> that represents the softplus activation function.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuronNode), Name = "MPSCNNNeuronSoftPlusNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnNeuronSoftPlusNode {
		[Static]
		[Export ("nodeWithSource:a:b:")]
		MPSCnnNeuronSoftPlusNode Create (MPSNNImageNode sourceNode, float a, float b);

		[Export ("initWithSource:a:b:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, float a, float b);

		[Static]
		[Export ("nodeWithSource:")]
		MPSCnnNeuronSoftPlusNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnNeuronNode" /> that represents the softsign activation function.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuronNode), Name = "MPSCNNNeuronSoftSignNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnNeuronSoftSignNode {
		[Static]
		[Export ("nodeWithSource:")]
		MPSCnnNeuronSoftSignNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	/// <summary>A <format type="text/html"><a href="https://docs.microsoft.com/en-us/search/index?search=Metal%20Performance%20Shaders%20MPSCNNNeuron%20Node&amp;scope=Xamarin" title="T:MetalPerformanceShaders.MPSCNNNeuronNode">T:MetalPerformanceShaders.MPSCNNNeuronNode</a></format> that represents a tanh activation function.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuronNode), Name = "MPSCNNNeuronTanHNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnNeuronTanHNode {
		[Static]
		[Export ("nodeWithSource:a:b:")]
		MPSCnnNeuronTanHNode Create (MPSNNImageNode sourceNode, float a, float b);

		[Export ("initWithSource:a:b:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, float a, float b);

		[Static]
		[Export ("nodeWithSource:")]
		MPSCnnNeuronTanHNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnNeuronNode" /> that represents the parametric rectified linear unit activation function.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNeuronNode), Name = "MPSCNNNeuronPReLUNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnNeuronPReLUNode {
		[Static]
		[Export ("nodeWithSource:aData:")]
		MPSCnnNeuronPReLUNode Create (MPSNNImageNode sourceNode, NSData aData);

		[Export ("initWithSource:aData:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, NSData aData);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSNNFilterNode" /> that represents a pooling kernel in a convolutional neural net.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode), Name = "MPSCNNPoolingNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnPoolingNode {

		[MacCatalyst (13, 1)]
		[Export ("kernelWidth")]
		nuint KernelWidth { get; }

		[MacCatalyst (13, 1)]
		[Export ("kernelHeight")]
		nuint KernelHeight { get; }

		[MacCatalyst (13, 1)]
		[Export ("strideInPixelsX")]
		nuint StrideInPixelsX { get; }

		[MacCatalyst (13, 1)]
		[Export ("strideInPixelsY")]
		nuint StrideInPixelsY { get; }

		[Static]
		[Export ("nodeWithSource:filterSize:")]
		MPSCnnPoolingNode Create (MPSNNImageNode sourceNode, nuint size);

		[Static]
		[Export ("nodeWithSource:filterSize:stride:")]
		MPSCnnPoolingNode Create (MPSNNImageNode sourceNode, nuint size, nuint stride);

		[Export ("initWithSource:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY);

		[Export ("initWithSource:filterSize:stride:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint size, nuint stride);

		[Export ("initWithSource:filterSize:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint size);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientFilterNode), Name = "MPSCNNPoolingGradientNode")]
	[DisableDefaultCtor]
	interface MPSCnnPoolingGradientNode {

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:paddingPolicy:")]
		MPSCnnPoolingGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY, [NullAllowed] IMPSNNPadding paddingPolicy);

		[Export ("initWithSourceGradient:sourceImage:gradientState:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:paddingPolicy:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY, [NullAllowed] IMPSNNPadding paddingPolicy);

		[Export ("kernelWidth")]
		nuint KernelWidth { get; }

		[Export ("kernelHeight")]
		nuint KernelHeight { get; }

		[Export ("strideInPixelsX")]
		nuint StrideInPixelsX { get; }

		[Export ("strideInPixelsY")]
		nuint StrideInPixelsY { get; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnPoolingGradientNode), Name = "MPSCNNPoolingMaxGradientNode")]
	[DisableDefaultCtor]
	interface MPSCnnPoolingMaxGradientNode {

		[Static]
		[New]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:paddingPolicy:")]
		MPSCnnPoolingMaxGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY, [NullAllowed] IMPSNNPadding paddingPolicy);

		[Export ("initWithSourceGradient:sourceImage:gradientState:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:paddingPolicy:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY, [NullAllowed] IMPSNNPadding paddingPolicy);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnPoolingGradientNode), Name = "MPSCNNPoolingAverageGradientNode")]
	[DisableDefaultCtor]
	interface MPSCnnPoolingAverageGradientNode {

		[Static]
		[New]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:paddingPolicy:")]
		MPSCnnPoolingAverageGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY, [NullAllowed] IMPSNNPadding paddingPolicy);

		[Export ("initWithSourceGradient:sourceImage:gradientState:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:paddingPolicy:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY, [NullAllowed] IMPSNNPadding paddingPolicy);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnPoolingGradientNode), Name = "MPSCNNPoolingL2NormGradientNode")]
	[DisableDefaultCtor]
	interface MPSCnnPoolingL2NormGradientNode {

		[Static]
		[New]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:paddingPolicy:")]
		MPSCnnPoolingL2NormGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY, [NullAllowed] IMPSNNPadding paddingPolicy);

		[Export ("initWithSourceGradient:sourceImage:gradientState:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:paddingPolicy:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY, [NullAllowed] IMPSNNPadding paddingPolicy);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnPoolingGradientNode), Name = "MPSCNNDilatedPoolingMaxGradientNode")]
	[DisableDefaultCtor]
	interface MPSCnnDilatedPoolingMaxGradientNode {

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:dilationRateX:dilationRateY:")]
		MPSCnnDilatedPoolingMaxGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY, nuint dilationRateX, nuint dilationRateY);

		[Export ("initWithSourceGradient:sourceImage:gradientState:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:dilationRateX:dilationRateY:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY, nuint dilationRateX, nuint dilationRateY);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("dilationRateX")]
		nuint DilationRateX { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("dilationRateY")]
		nuint DilationRateY { get; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientFilterNode), Name = "MPSCNNSpatialNormalizationGradientNode")]
	[DisableDefaultCtor]
	interface MPSCnnSpatialNormalizationGradientNode {

		[Export ("kernelWidth")]
		nuint KernelWidth { get; set; }

		[Export ("kernelHeight")]
		nuint KernelHeight { get; set; }

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:kernelSize:")]
		MPSCnnSpatialNormalizationGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, nuint kernelSize);

		[Export ("initWithSourceGradient:sourceImage:gradientState:kernelSize:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, nuint kernelSize);

		[Export ("alpha")]
		float Alpha { get; set; }

		[Export ("beta")]
		float Beta { get; set; }

		[Export ("delta")]
		float Delta { get; set; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientFilterNode), Name = "MPSCNNLocalContrastNormalizationGradientNode")]
	[DisableDefaultCtor]
	interface MPSCnnLocalContrastNormalizationGradientNode {

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:kernelWidth:kernelHeight:")]
		MPSCnnLocalContrastNormalizationGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, nuint kernelWidth, nuint kernelHeight);

		[Export ("initWithSourceGradient:sourceImage:gradientState:kernelWidth:kernelHeight:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, nuint kernelWidth, nuint kernelHeight);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("alpha")]
		float Alpha { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("beta")]
		float Beta { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("delta")]
		float Delta { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("p0")]
		float P0 { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("pm")]
		float Pm { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("ps")]
		float Ps { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("kernelWidth")]
		nuint KernelWidth { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("kernelHeight")]
		nuint KernelHeight { get; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientFilterNode), Name = "MPSCNNCrossChannelNormalizationGradientNode")]
	[DisableDefaultCtor]
	interface MPSCnnCrossChannelNormalizationGradientNode {

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:kernelSize:")]
		MPSCnnCrossChannelNormalizationGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, nuint kernelSize);

		[Export ("initWithSourceGradient:sourceImage:gradientState:kernelSize:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, nuint kernelSize);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("kernelSize")]
		nuint KernelSize { get; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode), Name = "MPSCNNInstanceNormalizationNode")]
	[DisableDefaultCtor]
	interface MPSCnnInstanceNormalizationNode {

		[Static]
		[Export ("nodeWithSource:dataSource:")]
		MPSCnnInstanceNormalizationNode Create (MPSNNImageNode source, IMPSCnnInstanceNormalizationDataSource dataSource);

		[Export ("initWithSource:dataSource:")]
		NativeHandle Constructor (MPSNNImageNode source, IMPSCnnInstanceNormalizationDataSource dataSource);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientFilterNode), Name = "MPSCNNInstanceNormalizationGradientNode")]
	[DisableDefaultCtor]
	interface MPSCnnInstanceNormalizationGradientNode : MPSNNTrainableNode {

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:")]
		MPSCnnInstanceNormalizationGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState);

		[Export ("initWithSourceGradient:sourceImage:gradientState:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode), Name = "MPSCNNBatchNormalizationNode")]
	[DisableDefaultCtor]
	interface MPSCnnBatchNormalizationNode {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("flags", ArgumentSemantic.Assign)]
		MPSCnnBatchNormalizationFlags Flags { get; set; }

		[Static]
		[Export ("nodeWithSource:dataSource:")]
		MPSCnnBatchNormalizationNode Create (MPSNNImageNode source, IMPSCnnBatchNormalizationDataSource dataSource);

		[Export ("initWithSource:dataSource:")]
		NativeHandle Constructor (MPSNNImageNode source, IMPSCnnBatchNormalizationDataSource dataSource);
	}

	[MacCatalyst (13, 1)]
	[DisableDefaultCtor]
	[BaseType (typeof (MPSNNGradientFilterNode), Name = "MPSCNNBatchNormalizationGradientNode")]
	interface MPSCnnBatchNormalizationGradientNode : MPSNNTrainableNode {

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:")]
		MPSCnnBatchNormalizationGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState);

		[Export ("initWithSourceGradient:sourceImage:gradientState:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSNNFilterNode" /> that represents a dilated max pooling filter.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode), Name = "MPSCNNDilatedPoolingMaxNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnDilatedPoolingMaxNode {
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("dilationRateX")]
		nuint DilationRateX { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("dilationRateY")]
		nuint DilationRateY { get; }

		[Static]
		[Export ("nodeWithSource:filterSize:")]
		MPSCnnDilatedPoolingMaxNode Create (MPSNNImageNode sourceNode, nuint size);

		[Static]
		[Export ("nodeWithSource:filterSize:stride:dilationRate:")]
		MPSCnnDilatedPoolingMaxNode Create (MPSNNImageNode sourceNode, nuint size, nuint stride, nuint dilationRate);

		[Export ("initWithSource:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:dilationRateX:dilationRateY:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY, nuint dilationRateX, nuint dilationRateY);

		[Export ("initWithSource:filterSize:stride:dilationRate:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint size, nuint stride, nuint dilationRate);

		[Export ("initWithSource:filterSize:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint size);
	}

	/// <summary>Abstract base class for normalization <see cref="T:MetalPerformanceShaders.MPSNNFilterNode" /> subclasses.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode), Name = "MPSCNNNormalizationNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnNormalizationNode {
		[Export ("alpha")]
		float Alpha { get; set; }

		[Export ("beta")]
		float Beta { get; set; }

		[Export ("delta")]
		float Delta { get; set; }

		[Static]
		[Export ("nodeWithSource:")]
		MPSCnnNormalizationNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		[DesignatedInitializer]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnNormalizationNode" /> that represents a spatial normalization kernel.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNormalizationNode), Name = "MPSCNNSpatialNormalizationNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnSpatialNormalizationNode {
		[Export ("kernelWidth")]
		nuint KernelWidth { get; set; }

		[Export ("kernelHeight")]
		nuint KernelHeight { get; set; }

		[Static]
		[Export ("nodeWithSource:kernelSize:")]
		MPSCnnSpatialNormalizationNode Create (MPSNNImageNode sourceNode, nuint kernelSize);

		[Export ("initWithSource:kernelSize:")]
		[DesignatedInitializer]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint kernelSize);

		[Export ("initWithSource:")]
		[DesignatedInitializer]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnNormalizationNode" /> representing a local contrast normalization kernel.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNormalizationNode), Name = "MPSCNNLocalContrastNormalizationNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnLocalContrastNormalizationNode {
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("pm")]
		float Pm { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("ps")]
		float Ps { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("p0")]
		float P0 { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("kernelWidth")]
		nuint KernelWidth { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("kernelHeight")]
		nuint KernelHeight { get; set; }

		[Static]
		[Export ("nodeWithSource:kernelSize:")]
		MPSCnnLocalContrastNormalizationNode Create (MPSNNImageNode sourceNode, nuint kernelSize);

		[Export ("initWithSource:kernelSize:")]
		[DesignatedInitializer]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint kernelSize);

		[Export ("initWithSource:")]
		[DesignatedInitializer]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnNormalizationNode" /> that represents a cross-channel normalization kernel.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnNormalizationNode), Name = "MPSCNNCrossChannelNormalizationNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnCrossChannelNormalizationNode {
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("kernelSizeInFeatureChannels")]
		nuint KernelSizeInFeatureChannels { get; set; }

		[Static]
		[Export ("nodeWithSource:kernelSize:")]
		MPSCnnCrossChannelNormalizationNode Create (MPSNNImageNode sourceNode, nuint kernelSize);

		[Export ("initWithSource:kernelSize:")]
		[DesignatedInitializer]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint kernelSize);

		[Export ("initWithSource:")]
		[DesignatedInitializer]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	/// <summary>Abstract neural network graph node for image resampling.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode))]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSNNScaleNode {
		[Static]
		[Export ("nodeWithSource:outputSize:")]
		MPSNNScaleNode Create (MPSNNImageNode sourceNode, MTLSize size);

		[Static]
		[Export ("nodeWithSource:transformProvider:outputSize:")]
		MPSNNScaleNode Create (MPSNNImageNode sourceNode, [NullAllowed] IMPSImageTransformProvider transformProvider, MTLSize size);

		[Export ("initWithSource:outputSize:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, MTLSize size);

		[Export ("initWithSource:transformProvider:outputSize:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, [NullAllowed] IMPSImageTransformProvider transformProvider, MTLSize size);
	}

	/// <summary>Abstract base class of arithmetic nodes.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode))]
	[DisableDefaultCtor]
	interface MPSNNBinaryArithmeticNode {
		[Static]
		[Export ("nodeWithSources:")]
		MPSNNBinaryArithmeticNode Create (MPSNNImageNode [] sourceNodes);

		[Static]
		[Export ("nodeWithLeftSource:rightSource:")]
		MPSNNBinaryArithmeticNode Create (MPSNNImageNode left, MPSNNImageNode right);

		[Export ("initWithSources:")]
		NativeHandle Constructor (MPSNNImageNode [] sourceNodes);

		[Export ("initWithLeftSource:rightSource:")]
		NativeHandle Constructor (MPSNNImageNode left, MPSNNImageNode right);

		[MacCatalyst (13, 1)]
		[Export ("gradientClass")]
		Class GradientClass { get; }

		[MacCatalyst (13, 1)]
		[Export ("gradientFiltersWithSources:")]
		MPSNNGradientFilterNode [] GetGradientFilters (MPSNNImageNode [] gradientImages);

		[Export ("primaryScale")]
		float PrimaryScale { get; set; }

		[Export ("secondaryScale")]
		float SecondaryScale { get; set; }

		[Export ("bias")]
		float Bias { get; set; }

		[Export ("primaryStrideInPixelsX")]
		nuint PrimaryStrideInPixelsX { get; set; }

		[Export ("primaryStrideInPixelsY")]
		nuint PrimaryStrideInPixelsY { get; set; }

		[Export ("primaryStrideInFeatureChannels")]
		nuint PrimaryStrideInFeatureChannels { get; set; }

		[Export ("secondaryStrideInPixelsX")]
		nuint SecondaryStrideInPixelsX { get; set; }

		[Export ("secondaryStrideInPixelsY")]
		nuint SecondaryStrideInPixelsY { get; set; }

		[Export ("secondaryStrideInFeatureChannels")]
		nuint SecondaryStrideInFeatureChannels { get; set; }

		[Export ("minimumValue")]
		float MinimumValue { get; set; }

		[Export ("maximumValue")]
		float MaximumValue { get; set; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNBinaryArithmeticNode))]
	[DisableDefaultCtor]
	interface MPSNNComparisonNode {

		[Export ("comparisonType", ArgumentSemantic.Assign)]
		MPSNNComparisonType ComparisonType { get; set; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientFilterNode))]
	[DisableDefaultCtor]
	interface MPSNNArithmeticGradientNode {

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:isSecondarySourceFilter:")]
		MPSNNArithmeticGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNBinaryGradientStateNode gradientState, bool isSecondarySourceFilter);

		[Export ("initWithSourceGradient:sourceImage:gradientState:isSecondarySourceFilter:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNBinaryGradientStateNode gradientState, bool isSecondarySourceFilter);

		[Export ("initWithGradientImages:forwardFilter:isSecondarySourceFilter:")]
		NativeHandle Constructor (MPSNNImageNode [] gradientImages, MPSNNFilterNode filter, bool isSecondarySourceFilter);

		[Export ("primaryScale")]
		float PrimaryScale { get; set; }

		[Export ("secondaryScale")]
		float SecondaryScale { get; set; }

		[Export ("bias")]
		float Bias { get; set; }

		[Export ("secondaryStrideInPixelsX")]
		nuint SecondaryStrideInPixelsX { get; set; }

		[Export ("secondaryStrideInPixelsY")]
		nuint SecondaryStrideInPixelsY { get; set; }

		[Export ("secondaryStrideInFeatureChannels")]
		nuint SecondaryStrideInFeatureChannels { get; set; }

		[Export ("minimumValue")]
		float MinimumValue { get; set; }

		[Export ("maximumValue")]
		float MaximumValue { get; set; }

		[Export ("isSecondarySourceFilter")]
		bool IsSecondarySourceFilter { get; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNArithmeticGradientNode))]
	[DisableDefaultCtor]
	interface MPSNNAdditionGradientNode {

		[Static]
		[New]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:isSecondarySourceFilter:")]
		MPSNNAdditionGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNBinaryGradientStateNode gradientState, bool isSecondarySourceFilter);

		[Export ("initWithSourceGradient:sourceImage:gradientState:isSecondarySourceFilter:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNBinaryGradientStateNode gradientState, bool isSecondarySourceFilter);

		[Export ("initWithGradientImages:forwardFilter:isSecondarySourceFilter:")]
		NativeHandle Constructor (MPSNNImageNode [] gradientImages, MPSNNFilterNode filter, bool isSecondarySourceFilter);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNArithmeticGradientNode))]
	[DisableDefaultCtor]
	interface MPSNNSubtractionGradientNode {

		[Static]
		[New]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:isSecondarySourceFilter:")]
		MPSNNSubtractionGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNBinaryGradientStateNode gradientState, bool isSecondarySourceFilter);

		[Export ("initWithSourceGradient:sourceImage:gradientState:isSecondarySourceFilter:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNBinaryGradientStateNode gradientState, bool isSecondarySourceFilter);

		[Export ("initWithGradientImages:forwardFilter:isSecondarySourceFilter:")]
		NativeHandle Constructor (MPSNNImageNode [] gradientImages, MPSNNFilterNode filter, bool isSecondarySourceFilter);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNArithmeticGradientNode))]
	[DisableDefaultCtor]
	interface MPSNNMultiplicationGradientNode {

		[Static]
		[New]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:isSecondarySourceFilter:")]
		MPSNNMultiplicationGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNBinaryGradientStateNode gradientState, bool isSecondarySourceFilter);

		[Export ("initWithSourceGradient:sourceImage:gradientState:isSecondarySourceFilter:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNBinaryGradientStateNode gradientState, bool isSecondarySourceFilter);

		[Export ("initWithGradientImages:forwardFilter:isSecondarySourceFilter:")]
		NativeHandle Constructor (MPSNNImageNode [] gradientImages, MPSNNFilterNode filter, bool isSecondarySourceFilter);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode), Name = "MPSCNNDropoutNode")]
	[DisableDefaultCtor]
	interface MPSCnnDropoutNode {

		[Static]
		[Export ("nodeWithSource:")]
		MPSCnnDropoutNode Create (MPSNNImageNode source);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode source);

		[Static]
		[Export ("nodeWithSource:keepProbability:")]
		MPSCnnDropoutNode Create (MPSNNImageNode source, float keepProbability);

		[Export ("initWithSource:keepProbability:")]
		NativeHandle Constructor (MPSNNImageNode source, float keepProbability);

		[Static]
		[Export ("nodeWithSource:keepProbability:seed:maskStrideInPixels:")]
		MPSCnnDropoutNode Create (MPSNNImageNode source, float keepProbability, nuint seed, MTLSize maskStrideInPixels);

		[Export ("initWithSource:keepProbability:seed:maskStrideInPixels:")]
		[DesignatedInitializer]
		NativeHandle Constructor (MPSNNImageNode source, float keepProbability, nuint seed, MTLSize maskStrideInPixels);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("keepProbability")]
		float KeepProbability { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("seed")]
		nuint Seed { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("maskStrideInPixels")]
		MTLSize MaskStrideInPixels { get; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientFilterNode), Name = "MPSCNNDropoutGradientNode")]
	[DisableDefaultCtor]
	interface MPSCnnDropoutGradientNode {

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:keepProbability:seed:maskStrideInPixels:")]
		MPSCnnDropoutGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, float keepProbability, nuint seed, MTLSize maskStrideInPixels);

		[Export ("initWithSourceGradient:sourceImage:gradientState:keepProbability:seed:maskStrideInPixels:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, float keepProbability, nuint seed, MTLSize maskStrideInPixels);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("keepProbability")]
		float KeepProbability { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("seed")]
		nuint Seed { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("maskStrideInPixels")]
		MTLSize MaskStrideInPixels { get; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNStateNode))]
	[DisableDefaultCtor]
	interface MPSNNLabelsNode {

	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode), Name = "MPSCNNLossNode")]
	[DisableDefaultCtor]
	interface MPSCnnLossNode {

		[Static]
		[Export ("nodeWithSource:lossDescriptor:")]
		MPSCnnLossNode Create (MPSNNImageNode source, MPSCnnLossDescriptor descriptor);

		[Export ("initWithSource:lossDescriptor:")]
		NativeHandle Constructor (MPSNNImageNode source, MPSCnnLossDescriptor descriptor);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("inputLabels", ArgumentSemantic.Retain)]
		MPSNNLabelsNode InputLabels { get; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode), Name = "MPSCNNYOLOLossNode")]
	[DisableDefaultCtor]
	interface MPSCnnYoloLossNode {

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("nodeWithSource:lossDescriptor:")]
		MPSCnnYoloLossNode Create (MPSNNImageNode source, MPSCnnYoloLossDescriptor descriptor);

		[Export ("initWithSource:lossDescriptor:")]
		NativeHandle Constructor (MPSNNImageNode source, MPSCnnYoloLossDescriptor descriptor);

		[Export ("inputLabels", ArgumentSemantic.Retain)]
		MPSNNLabelsNode InputLabels { get; }
	}

	/// <summary>Concatenates the results of two kernels.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode))]
	[DisableDefaultCtor]
	interface MPSNNConcatenationNode {
		[Static]
		[Export ("nodeWithSources:")]
		MPSNNConcatenationNode Create (MPSNNImageNode [] sourceNodes);

		[Export ("initWithSources:")]
		NativeHandle Constructor (MPSNNImageNode [] sourceNodes);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientFilterNode))]
	[DisableDefaultCtor]
	interface MPSNNConcatenationGradientNode {

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:")]
		MPSNNConcatenationGradientNode Create (MPSNNImageNode gradientSourceNode, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState);

		[Export ("initWithSourceGradient:sourceImage:gradientState:")]
		NativeHandle Constructor (MPSNNImageNode gradientSourceNode, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode))]
	[DisableDefaultCtor]
	interface MPSNNReshapeNode {

		[Static]
		[Export ("nodeWithSource:resultWidth:resultHeight:resultFeatureChannels:")]
		MPSNNReshapeNode Create (MPSNNImageNode source, nuint resultWidth, nuint resultHeight, nuint resultFeatureChannels);

		[Export ("initWithSource:resultWidth:resultHeight:resultFeatureChannels:")]
		NativeHandle Constructor (MPSNNImageNode source, nuint resultWidth, nuint resultHeight, nuint resultFeatureChannels);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientFilterNode))]
	[DisableDefaultCtor]
	interface MPSNNReshapeGradientNode {

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:")]
		MPSNNReshapeGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState);

		[Export ("initWithSourceGradient:sourceImage:gradientState:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientFilterNode))]
	[DisableDefaultCtor]
	interface MPSNNReductionSpatialMeanGradientNode {

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:")]
		MPSNNReductionSpatialMeanGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState);

		[Export ("initWithSourceGradient:sourceImage:gradientState:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode))]
	[DisableDefaultCtor]
	interface MPSNNPadNode {

		[Export ("fillValue")]
		float FillValue { get; set; }

		[Static]
		[Export ("nodeWithSource:paddingSizeBefore:paddingSizeAfter:edgeMode:")]
		MPSNNPadNode Create (MPSNNImageNode source, MPSImageCoordinate paddingSizeBefore, MPSImageCoordinate paddingSizeAfter, MPSImageEdgeMode edgeMode);

		[Export ("initWithSource:paddingSizeBefore:paddingSizeAfter:edgeMode:")]
		NativeHandle Constructor (MPSNNImageNode source, MPSImageCoordinate paddingSizeBefore, MPSImageCoordinate paddingSizeAfter, MPSImageEdgeMode edgeMode);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientFilterNode))]
	[DisableDefaultCtor]
	interface MPSNNPadGradientNode {

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:")]
		MPSNNPadGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState);

		[Export ("initWithSourceGradient:sourceImage:gradientState:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientFilterNode), Name = "MPSCNNSoftMaxGradientNode")]
	[DisableDefaultCtor]
	interface MPSCnnSoftMaxGradientNode {

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:")]
		MPSCnnSoftMaxGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState);

		[Export ("initWithSourceGradient:sourceImage:gradientState:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientFilterNode), Name = "MPSCNNLogSoftMaxGradientNode")]
	[DisableDefaultCtor]
	interface MPSCnnLogSoftMaxGradientNode {

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:")]
		MPSCnnLogSoftMaxGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState);

		[Export ("initWithSourceGradient:sourceImage:gradientState:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSNNFilterNode" /> that represents a softmax filter.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode), Name = "MPSCNNSoftMaxNode")]
	[DisableDefaultCtor]
	interface MPSCnnSoftMaxNode {
		[Static]
		[Export ("nodeWithSource:")]
		MPSCnnSoftMaxNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSNNFilterNode" /> that represents the logarithmic softmax loss function.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode), Name = "MPSCNNLogSoftMaxNode")]
	[DisableDefaultCtor]
	interface MPSCnnLogSoftMaxNode {
		[Static]
		[Export ("nodeWithSource:")]
		MPSCnnLogSoftMaxNode Create (MPSNNImageNode sourceNode);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSNNFilterNode" /> that performs nearest spatial upsampling.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode), Name = "MPSCNNUpsamplingNearestNode")]
	[DisableDefaultCtor]
	interface MPSCnnUpsamplingNearestNode {
		[Static]
		[Export ("nodeWithSource:integerScaleFactorX:integerScaleFactorY:")]
		MPSCnnUpsamplingNearestNode Create (MPSNNImageNode sourceNode, nuint integerScaleFactorX, nuint integerScaleFactorY);

		[Export ("initWithSource:integerScaleFactorX:integerScaleFactorY:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint integerScaleFactorX, nuint integerScaleFactorY);

		[Export ("scaleFactorX")]
		double ScaleFactorX { get; }

		[Export ("scaleFactorY")]
		double ScaleFactorY { get; }
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSNNFilterNode" /> that performs bilinear spatial upsampling.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode), Name = "MPSCNNUpsamplingBilinearNode")]
	[DisableDefaultCtor]
	interface MPSCnnUpsamplingBilinearNode {
		[Static]
		[Export ("nodeWithSource:integerScaleFactorX:integerScaleFactorY:")]
		MPSCnnUpsamplingBilinearNode Create (MPSNNImageNode sourceNode, nuint integerScaleFactorX, nuint integerScaleFactorY);

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("nodeWithSource:integerScaleFactorX:integerScaleFactorY:alignCorners:")]
		MPSCnnUpsamplingBilinearNode Create (MPSNNImageNode sourceNode, nuint integerScaleFactorX, nuint integerScaleFactorY, bool alignCorners);

		[Export ("initWithSource:integerScaleFactorX:integerScaleFactorY:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint integerScaleFactorX, nuint integerScaleFactorY);

		[MacCatalyst (13, 1)]
		[Export ("initWithSource:integerScaleFactorX:integerScaleFactorY:alignCorners:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint integerScaleFactorX, nuint integerScaleFactorY, bool alignCorners);

		[Export ("scaleFactorX")]
		double ScaleFactorX { get; }

		[Export ("scaleFactorY")]
		double ScaleFactorY { get; }

		[MacCatalyst (13, 1)]
		[Export ("alignCorners")]
		bool AlignCorners { get; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientFilterNode), Name = "MPSCNNUpsamplingNearestGradientNode")]
	[DisableDefaultCtor]
	interface MPSCnnUpsamplingNearestGradientNode {

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:scaleFactorX:scaleFactorY:")]
		MPSCnnUpsamplingNearestGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, double scaleFactorX, double scaleFactorY);

		[Export ("initWithSourceGradient:sourceImage:gradientState:scaleFactorX:scaleFactorY:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, double scaleFactorX, double scaleFactorY);

		[Export ("scaleFactorX")]
		double ScaleFactorX { get; }

		[Export ("scaleFactorY")]
		double ScaleFactorY { get; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientFilterNode), Name = "MPSCNNUpsamplingBilinearGradientNode")]
	[DisableDefaultCtor]
	interface MPSCnnUpsamplingBilinearGradientNode {

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:gradientState:scaleFactorX:scaleFactorY:")]
		MPSCnnUpsamplingBilinearGradientNode NodeWithSourceGradient (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, double scaleFactorX, double scaleFactorY);

		[Export ("initWithSourceGradient:sourceImage:gradientState:scaleFactorX:scaleFactorY:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNGradientStateNode gradientState, double scaleFactorX, double scaleFactorY);

		[Export ("scaleFactorX")]
		double ScaleFactorX { get; }

		[Export ("scaleFactorY")]
		double ScaleFactorY { get; }
	}

	interface IMPSNNLossCallback { }

	[Protocol]
	interface MPSNNLossCallback : NSSecureCoding, NSCopying {
		[Abstract]
		[Export ("scalarWeightForSourceImage:destinationImage:")]
		float GetScalarWeight (MPSImage sourceImage, MPSImage destinationImage);
	}

	[DisableDefaultCtor]
	[TV (13, 0), iOS (13, 0)]
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode))]
	interface MPSNNForwardLossNode {
		[Export ("lossType")]
		MPSCnnLossType LossType { get; }

		[Export ("reductionType")]
		MPSCnnReductionType ReductionType { get; }

		[Export ("numberOfClasses")]
		nuint NumberOfClasses { get; }

		[Export ("weight")]
		float Weight { get; }

		[Export ("labelSmoothing")]
		float LabelSmoothing { get; }

		[Export ("epsilon")]
		float Epsilon { get; }

		[Export ("delta")]
		float Delta { get; }

		[NullAllowed, Export ("propertyCallBack", ArgumentSemantic.Retain)]
		IMPSNNLossCallback PropertyCallBack { get; set; }

		[Static]
		[Export ("nodeWithSource:labels:weights:lossDescriptor:")]
		MPSNNForwardLossNode Create (MPSNNImageNode source, MPSNNImageNode labels, MPSNNImageNode weights, MPSCnnLossDescriptor descriptor);

		[Static]
		[Export ("nodeWithSource:labels:lossDescriptor:")]
		MPSNNForwardLossNode Create (MPSNNImageNode source, MPSNNImageNode labels, MPSCnnLossDescriptor descriptor);

		[Static]
		[Export ("nodeWithSources:lossDescriptor:")]
		MPSNNForwardLossNode Create (MPSNNImageNode [] sourceNodes, MPSCnnLossDescriptor descriptor);

		[Export ("initWithSource:labels:weights:lossDescriptor:")]
		NativeHandle Constructor (MPSNNImageNode source, MPSNNImageNode labels, [NullAllowed] MPSNNImageNode weights, MPSCnnLossDescriptor descriptor);

		[Export ("initWithSource:labels:lossDescriptor:")]
		NativeHandle Constructor (MPSNNImageNode source, MPSNNImageNode labels, MPSCnnLossDescriptor descriptor);

		[Export ("initWithSources:lossDescriptor:")]
		NativeHandle Constructor (MPSNNImageNode [] sourceNodes, MPSCnnLossDescriptor descriptor);

		[Export ("gradientFilterWithSources:")]
		MPSNNLossGradientNode GetFilter (MPSNNImageNode [] sourceGradient);

		[Export ("gradientFiltersWithSources:")]
		MPSNNLossGradientNode [] GetFilters (MPSNNImageNode [] sourceGradient);

		[Export ("gradientFilterWithSource:")]
		MPSNNLossGradientNode GetFilter (MPSNNImageNode sourceGradient);

		[Export ("gradientFiltersWithSource:")]
		MPSNNLossGradientNode [] GetFilters (MPSNNImageNode sourceGradient);
	}

	[DisableDefaultCtor]
	[TV (13, 0), iOS (13, 0)]
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientFilterNode))]
	interface MPSNNLossGradientNode {
		[Export ("lossType")]
		MPSCnnLossType LossType { get; }

		[Export ("reductionType")]
		MPSCnnReductionType ReductionType { get; }

		[Export ("numberOfClasses")]
		nuint NumberOfClasses { get; }

		[Export ("weight")]
		float Weight { get; }

		[Export ("labelSmoothing")]
		float LabelSmoothing { get; }

		[Export ("epsilon")]
		float Epsilon { get; }

		[Export ("delta")]
		float Delta { get; }

		[Export ("isLabelsGradientFilter")]
		bool IsLabelsGradientFilter { get; }

		[NullAllowed, Export ("propertyCallBack", ArgumentSemantic.Retain)]
		IMPSNNLossCallback PropertyCallBack { get; set; }

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:labels:weights:gradientState:lossDescriptor:isLabelsGradientFilter:")]
		MPSNNLossGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNImageNode labels, MPSNNImageNode weights, [NullAllowed] MPSNNGradientStateNode gradientState, MPSCnnLossDescriptor descriptor, bool isLabelsGradientFilter);

		[Static]
		[Export ("nodeWithSourceGradient:sourceImage:labels:gradientState:lossDescriptor:isLabelsGradientFilter:")]
		MPSNNLossGradientNode Create (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNImageNode labels, [NullAllowed] MPSNNGradientStateNode gradientState, MPSCnnLossDescriptor descriptor, bool isLabelsGradientFilter);

		[Static]
		[Export ("nodeWithSources:gradientState:lossDescriptor:isLabelsGradientFilter:")]
		MPSNNLossGradientNode Create (MPSNNImageNode [] sourceNodes, [NullAllowed] MPSNNGradientStateNode gradientState, MPSCnnLossDescriptor descriptor, bool isLabelsGradientFilter);

		[Export ("initWithSourceGradient:sourceImage:labels:weights:gradientState:lossDescriptor:isLabelsGradientFilter:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNImageNode labels, [NullAllowed] MPSNNImageNode weights, [NullAllowed] MPSNNGradientStateNode gradientState, MPSCnnLossDescriptor descriptor, bool isLabelsGradientFilter);

		[Export ("initWithSourceGradient:sourceImage:labels:gradientState:lossDescriptor:isLabelsGradientFilter:")]
		NativeHandle Constructor (MPSNNImageNode sourceGradient, MPSNNImageNode sourceImage, MPSNNImageNode labels, [NullAllowed] MPSNNGradientStateNode gradientState, MPSCnnLossDescriptor descriptor, bool isLabelsGradientFilter);

		[Export ("initWithSources:gradientState:lossDescriptor:isLabelsGradientFilter:")]
		NativeHandle Constructor (MPSNNImageNode [] sourceNodes, [NullAllowed] MPSNNGradientStateNode gradientState, MPSCnnLossDescriptor descriptor, bool isLabelsGradientFilter);
	}

	[DisableDefaultCtor]
	[TV (13, 0), iOS (13, 0)]
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNFilterNode))]
	interface MPSNNInitialGradientNode {
		[Static]
		[Export ("nodeWithSource:")]
		MPSNNInitialGradientNode Create (MPSNNImageNode source);

		[Export ("initWithSource:")]
		NativeHandle Constructor (MPSNNImageNode source);
	}

	/// <summary>An optimized neural network graph.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel))]
	[DisableDefaultCtor] // There is a DesignatedInitializer, file a bug if needed.
	interface MPSNNGraph : NSCopying, NSSecureCoding {

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:resultImage:resultImageIsNeeded:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSNNImageNode resultImage, bool resultIsNeeded);

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("graphWithDevice:resultImage:resultImageIsNeeded:")]
		[return: NullAllowed]
		MPSNNGraph Create (IMTLDevice device, MPSNNImageNode resultImage, bool resultIsNeeded);

		[Deprecated (PlatformName.TvOS, 11, 3, message: "Use '.ctor (IMTLDevice, MPSNNImageNode, bool)' instead.")]
		[Deprecated (PlatformName.iOS, 11, 3, message: "Use '.ctor (IMTLDevice, MPSNNImageNode, bool)' instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 13, 4, message: "Use '.ctor (IMTLDevice, MPSNNImageNode, bool)' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Use '.ctor (IMTLDevice, MPSNNImageNode, bool)' instead.")]
		[Export ("initWithDevice:resultImage:")]
		NativeHandle Constructor (IMTLDevice device, MPSNNImageNode resultImage);

		// Not added because the generated constructor is too hard to use
		// and there is an alternative Create method that accomplishes the same
		// with a bool[]
		// [TV (13,0), iOS (13,0)]
		// [Export ("initWithDevice:resultImages:resultsAreNeeded:")]
		// [DesignatedInitializer]
		// NativeHandle Constructor (IMTLDevice device, MPSNNImageNode[] resultImages, IntPtr resultsAreNeeded);

		[TV (13, 0), iOS (13, 0)]
		[MacCatalyst (13, 1)]
		[Static]
		[Export ("graphWithDevice:resultImages:resultsAreNeeded:")]
		[return: NullAllowed]
		MPSNNGraph Create (IMTLDevice device, MPSNNImageNode [] resultImages, IntPtr resultsAreNeeded);

		// Not added because it short lived a couple of minor releases and there are alternatives.
		//[Deprecated (PlatformName.TvOS, 11, 3)]
		//[Deprecated (PlatformName.iOS, 11, 3)]
		//[Deprecated (PlatformName.MacOSX, 10, 13, 4)]
		//[Static]
		//[Export ("graphWithDevice:resultImage:")]
		//[return: NullAllowed]
		//MPSNNGraph Create (IMTLDevice device, MPSNNImageNode resultImage);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("sourceImageHandles", ArgumentSemantic.Copy)]
		IMPSHandle [] SourceImageHandles { get; }

		/// <summary>To be added.</summary>
		///         <value>
		///           <para>(More documentation for this node is coming)</para>
		///           <para tool="nullallowed">This value can be <see langword="null" />.</para>
		///         </value>
		///         <remarks>To be added.</remarks>
		[NullAllowed, Export ("sourceStateHandles", ArgumentSemantic.Copy)]
		IMPSHandle [] SourceStateHandles { get; }

		/// <summary>To be added.</summary>
		///         <value>
		///           <para>(More documentation for this node is coming)</para>
		///           <para tool="nullallowed">This value can be <see langword="null" />.</para>
		///         </value>
		///         <remarks>To be added.</remarks>
		[NullAllowed, Export ("intermediateImageHandles", ArgumentSemantic.Copy)]
		IMPSHandle [] IntermediateImageHandles { get; }

		/// <summary>To be added.</summary>
		///         <value>
		///           <para>(More documentation for this node is coming)</para>
		///           <para tool="nullallowed">This value can be <see langword="null" />.</para>
		///         </value>
		///         <remarks>To be added.</remarks>
		[NullAllowed, Export ("resultStateHandles", ArgumentSemantic.Copy)]
		IMPSHandle [] ResultStateHandles { get; }

		/// <summary>To be added.</summary>
		///         <value>
		///           <para>(More documentation for this node is coming)</para>
		///           <para tool="nullallowed">This value can be <see langword="null" />.</para>
		///         </value>
		///         <remarks>To be added.</remarks>
		[NullAllowed, Export ("resultHandle")]
		IMPSHandle ResultHandle { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("outputStateIsTemporary")]
		bool IsOutputStateTemporary { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("destinationImageAllocator", ArgumentSemantic.Retain)]
		IMPSImageAllocator DestinationImageAllocator { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("format", ArgumentSemantic.Assign)]
		MPSImageFeatureChannelFormat Format { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("resultImageIsNeeded")]
		bool ResultImageIsNeeded { get; }

		[MacCatalyst (13, 1)]
		[Export ("reloadFromDataSources")]
		void ReloadFromDataSources ();

		[Export ("encodeToCommandBuffer:sourceImages:sourceStates:intermediateImages:destinationStates:")]
		[return: NullAllowed]
		MPSImage EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSImage [] sourceImages, [NullAllowed] MPSState [] sourceStates, [NullAllowed] NSMutableArray<MPSImage> intermediateImages, [NullAllowed] NSMutableArray<MPSState> destinationStates);

		[MacCatalyst (13, 1)]
		[Export ("encodeBatchToCommandBuffer:sourceImages:sourceStates:intermediateImages:destinationStates:")]
		[return: NullAllowed]
		NSArray<MPSImage> EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> [] sourceImages, [NullAllowed] NSArray<MPSState> [] sourceStates, [NullAllowed] NSMutableArray<NSArray<MPSImage>> intermediateImages, [NullAllowed] NSMutableArray<NSArray<MPSState>> destinationStates);

		[Export ("encodeToCommandBuffer:sourceImages:")]
		[return: NullAllowed]
		MPSImage EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSImage [] sourceImages);

		[Export ("encodeBatchToCommandBuffer:sourceImages:sourceStates:")]
		[return: NullAllowed]
		NSArray<MPSImage> EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> [] sourceImages, [NullAllowed] NSArray<MPSState> [] sourceStates);

		[Async, Export ("executeAsyncWithSourceImages:completionHandler:")]
		MPSImage Execute (MPSImage [] sourceImages, Action<MPSImage, NSError> handler);

		[MacCatalyst (13, 1)]
		[Export ("readCountForSourceImageAtIndex:")]
		nuint GetReadCountForSourceImage (nuint index);

		[MacCatalyst (13, 1)]
		[Export ("readCountForSourceStateAtIndex:")]
		nuint GetReadCountForSourceState (nuint index);
	}

	interface IMPSHandle { }

	/// <summary>Interface describing a Metal Performance Shaders-specific identifier.</summary>
	[MacCatalyst (13, 1)]
	[Protocol]
	interface MPSHandle : NSCoding {
		[Abstract]
		[NullAllowed, Export ("label")]
		string Label { get; }
	}

	/// <summary>Interface representing the required methods (if any) of the protocol <see cref="T:MetalPerformanceShaders.MPSCnnConvolutionDataSource" />.</summary>
	///     <remarks>
	///       <para>This interface contains the required methods (if any) from the protocol defined by <see cref="T:MetalPerformanceShaders.MPSCnnConvolutionDataSource" />.</para>
	///       <para>If developers create classes that implement this interface, the implementation methods will automatically be exported to Objective-C with the matching signature from the method defined in the <see cref="T:MetalPerformanceShaders.MPSCnnConvolutionDataSource" /> protocol.</para>
	///       <para>Optional methods (if any) are provided by the <see cref="T:MetalPerformanceShaders.MPSCnnConvolutionDataSource_Extensions" /> class as extension methods to the interface, allowing developers to invoke any optional methods on the protocol.</para>
	///     </remarks>
	interface IMPSCnnConvolutionDataSource { }

	/// <summary>Base class for classes that provide weights and bias terms to convolution filters.</summary>
	[MacCatalyst (13, 1)]
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "MPSCNNConvolutionDataSource")]
	interface MPSCnnConvolutionDataSource : NSCopying {
		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Abstract]
		[Export ("dataType")]
		MPSDataType DataType { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Abstract]
		[Export ("descriptor")]
		MPSCnnConvolutionDescriptor Descriptor { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Abstract]
		[Export ("weights")]
		IntPtr Weights { get; }

		/// <summary>To be added.</summary>
		///         <value>
		///           <para>(More documentation for this node is coming)</para>
		///           <para tool="nullallowed">This value can be <see langword="null" />.</para>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Abstract]
		[NullAllowed, Export ("biasTerms")]
		IntPtr /* float* */ BiasTerms { get; }

		[Abstract]
		[Export ("load")]
#if NET
		bool Load ();
#else
		bool Load { get; }
#endif

		[Abstract]
		[Export ("purge")]
		void Purge ();

		/// <summary>To be added.</summary>
		///         <value>
		///           <para>(More documentation for this node is coming)</para>
		///           <para tool="nullallowed">This value can be <see langword="null" />.</para>
		///         </value>
		///         <remarks>To be added.</remarks>
		[Abstract]
		[NullAllowed, Export ("label")]
		string Label { get; }

		[Export ("rangesForUInt8Kernel")]
		IntPtr GetRangesForUInt8Kernel ();

		[Export ("lookupTableForUInt8Kernel")]
		IntPtr /* float* */ GetLookupTableForUInt8Kernel ();

		[MacCatalyst (13, 1)]
		[Export ("weightsQuantizationType")]
		MPSCnnWeightsQuantizationType GetWeightsQuantizationType ();

		[MacCatalyst (13, 1)]
		[Export ("updateWithCommandBuffer:gradientState:sourceState:")]
		[return: NullAllowed]
		MPSCnnConvolutionWeightsAndBiasesState Update (IMTLCommandBuffer commandBuffer, MPSCnnConvolutionGradientState gradientState, MPSCnnConvolutionWeightsAndBiasesState sourceState);

		[MacCatalyst (13, 1)]
		[Export ("updateWithGradientState:sourceState:")]
		bool Update (MPSCnnConvolutionGradientState gradientState, MPSCnnConvolutionWeightsAndBiasesState sourceState);

		[MacCatalyst (13, 1)]
		[Export ("copyWithZone:device:")]
		[return: Release]
		IMPSCnnConvolutionDataSource Copy ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);

		[TV (14, 0), MacCatalyst (14, 0), iOS (14, 0)]
		[Export ("kernelWeightsDataType")]
		MPSDataType KernelWeightsDataType { get; }
	}

	interface IMPSNNPadding { }

	/// <summary>Interface describing how kernels should pad their inputs.</summary>
	[MacCatalyst (13, 1)]
	[Protocol]
	interface MPSNNPadding : NSSecureCoding {
		[Abstract]
		[Export ("paddingMethod")]
		MPSNNPaddingMethod PaddingMethod { get; }

		[Export ("label")]
		string GetLabel ();

		[Export ("destinationImageDescriptorForSourceImages:sourceStates:forKernel:suggestedDescriptor:")]
		MPSImageDescriptor GetDestinationImageDescriptor (MPSImage [] sourceImages, [NullAllowed] MPSState [] sourceStates, MPSKernel kernel, MPSImageDescriptor inDescriptor);

		[MacCatalyst (13, 1)]
		[Export ("inverse")]
		[return: NullAllowed]
		IMPSNNPadding GetInverse ();
	}

	interface IMPSImageSizeEncodingState { }

	/// <summary>Interface defining methods relating to when image size is stored elsewhere in the graph.</summary>
	[MacCatalyst (13, 1)]
	[Protocol]
	interface MPSImageSizeEncodingState {
		[Abstract]
		[Export ("sourceWidth")]
		nuint SourceWidth { get; }

		[Abstract]
		[Export ("sourceHeight")]
		nuint SourceHeight { get; }
	}

	interface IMPSImageAllocator { }

	/// <summary>Interface defining a factory that generates a <see cref="T:MetalPerformanceShaders.MPSImage" /> from a <see cref="T:MetalPerformanceShaders.IMTLCommandBuffer" />, a <see cref="T:MetalPerformanceShaders.MPSImageDescriptor" />, and a <see cref="T:MetalPerformanceShaders.MPSKernel" />.</summary>
	[MacCatalyst (13, 1)]
	[Protocol]
	interface MPSImageAllocator : NSSecureCoding {
		[Abstract]
		[Export ("imageForCommandBuffer:imageDescriptor:kernel:")]
		MPSImage GetImage (IMTLCommandBuffer cmdBuf, MPSImageDescriptor descriptor, MPSKernel kernel);

		[MacCatalyst (13, 1)]
		[Export ("imageBatchForCommandBuffer:imageDescriptor:kernel:count:")]
		NSArray<MPSImage> GetImageBatch (IMTLCommandBuffer commandBuffer, MPSImageDescriptor descriptor, MPSKernel kernel, nuint count);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNStateNode), Name = "MPSCNNConvolutionStateNode")]
	[DisableDefaultCtor]
	interface MPSCnnConvolutionStateNode {
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNStateNode))]
	[DisableDefaultCtor]
	interface MPSNNGradientStateNode {
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientStateNode), Name = "MPSCNNConvolutionGradientStateNode")]
	[DisableDefaultCtor]
	interface MPSCnnConvolutionGradientStateNode {
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNStateNode))]
	[DisableDefaultCtor]
	interface MPSNNBinaryGradientStateNode {
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNBinaryGradientStateNode))]
	[DisableDefaultCtor]
	interface MPSNNArithmeticGradientStateNode {
	}

	interface IMPSImageTransformProvider { }

	/// <summary>Interface defining image resampling methods.</summary>
	[MacCatalyst (13, 1)]
	[Protocol]
	interface MPSImageTransformProvider : NSCoding {
		[Abstract]
		[Export ("transformForSourceImage:handle:")]
		MPSScaleTransform GetTransform (MPSImage image, [NullAllowed] IMPSHandle handle);
	}

	[MacCatalyst (13, 1)]
	[Protocol]
	interface MPSDeviceProvider {
		[Abstract]
		[Export ("mpsMTLDevice")]
		IMTLDevice GetMTLDevice ();
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSNNFilterNode" /> that returns the average value of its input region.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnPoolingNode), Name = "MPSCNNPoolingAverageNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnPoolingAverageNode {
		[Export ("initWithSource:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY);

		[Export ("initWithSource:filterSize:stride:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint size, nuint stride);

		[Export ("initWithSource:filterSize:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint size);
	}

	/// <summary>Representation of an L2-norm pooling filter.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnPoolingNode), Name = "MPSCNNPoolingL2NormNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnPoolingL2NormNode {
		[Export ("initWithSource:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY);

		[Export ("initWithSource:filterSize:stride:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint size, nuint stride);

		[Export ("initWithSource:filterSize:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint size);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSCnnPoolingNode" /> that represents a max-pooling kernel in a convolutional neural net.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnPoolingNode), Name = "MPSCNNPoolingMaxNode")]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSCnnPoolingMaxNode {
		[Export ("initWithSource:kernelWidth:kernelHeight:strideInPixelsX:strideInPixelsY:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint kernelWidth, nuint kernelHeight, nuint strideInPixelsX, nuint strideInPixelsY);

		[Export ("initWithSource:filterSize:stride:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint size, nuint stride);

		[Export ("initWithSource:filterSize:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, nuint size);
	}

	/// <summary>Adds the results of two kernels.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNBinaryArithmeticNode))]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSNNAdditionNode {
		[Export ("initWithSources:")]
		NativeHandle Constructor (MPSNNImageNode [] sourceNodes);

		[Export ("initWithLeftSource:rightSource:")]
		NativeHandle Constructor (MPSNNImageNode left, MPSNNImageNode right);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSNNFilterNode" /> that performs bilinear sampling.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNScaleNode))]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSNNBilinearScaleNode {
		[Export ("initWithSource:outputSize:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, MTLSize size);

		[Export ("initWithSource:transformProvider:outputSize:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, [NullAllowed] IMPSImageTransformProvider transformProvider, MTLSize size);
	}

	/// <summary>Divides the results of two kernels.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNBinaryArithmeticNode))]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSNNDivisionNode {
		[Export ("initWithSources:")]
		NativeHandle Constructor (MPSNNImageNode [] sourceNodes);

		[Export ("initWithLeftSource:rightSource:")]
		NativeHandle Constructor (MPSNNImageNode left, MPSNNImageNode right);
	}

	/// <summary>A <see cref="T:MetalPerformanceShaders.MPSNNFilterNode" /> that performs Lanczos resampling.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNScaleNode))]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSNNLanczosScaleNode {
		[Export ("initWithSource:outputSize:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, MTLSize size);

		[Export ("initWithSource:transformProvider:outputSize:")]
		NativeHandle Constructor (MPSNNImageNode sourceNode, [NullAllowed] IMPSImageTransformProvider transformProvider, MTLSize size);
	}

	/// <summary>Multiplies the results of two kernels.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNBinaryArithmeticNode))]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSNNMultiplicationNode {
		[Export ("initWithSources:")]
		NativeHandle Constructor (MPSNNImageNode [] sourceNodes);

		[Export ("initWithLeftSource:rightSource:")]
		NativeHandle Constructor (MPSNNImageNode left, MPSNNImageNode right);
	}

	/// <summary>Subtracts the results of two kernels.</summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNBinaryArithmeticNode))]
	[DisableDefaultCtor] // 'init' is unavailable
	interface MPSNNSubtractionNode {
		[Export ("initWithSources:")]
		NativeHandle Constructor (MPSNNImageNode [] sourceNodes);

		[Export ("initWithLeftSource:rightSource:")]
		NativeHandle Constructor (MPSNNImageNode left, MPSNNImageNode right);
	}

	/// <summary>A one-dimensional array allocated in GPU private memory.
	/// </summary>
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSVector))]
	[DisableDefaultCtor]
	interface MPSTemporaryVector {

		[Static]
		[Export ("temporaryVectorWithCommandBuffer:descriptor:")]
		MPSTemporaryVector Create (IMTLCommandBuffer commandBuffer, MPSVectorDescriptor descriptor);

		[Static]
		[Export ("prefetchStorageWithCommandBuffer:descriptorList:")]
		void PrefetchStorage (IMTLCommandBuffer commandBuffer, MPSVectorDescriptor [] descriptorList);

		[Export ("readCount")]
		nuint ReadCount { get; set; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel))]
	[DisableDefaultCtor]
	interface MPSMatrixSum {

		[Export ("initWithDevice:count:rows:columns:transpose:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint count, nuint rows, nuint columns, bool transpose);

		[Export ("rows")]
		nuint Rows { get; }

		[Export ("columns")]
		nuint Columns { get; }

		[Export ("count")]
		nuint Count { get; }

		[Export ("transpose")]
		bool Transpose { get; }

		[Export ("setNeuronType:parameterA:parameterB:parameterC:")]
		void SetNeuronType (MPSCnnNeuronType neuronType, float parameterA, float parameterB, float parameterC);

		[Export ("neuronType")]
		MPSCnnNeuronType NeuronType { get; }

		[Export ("neuronParameterA")]
		float NeuronParameterA { get; }

		[Export ("neuronParameterB")]
		float NeuronParameterB { get; }

		[Export ("neuronParameterC")]
		float NeuronParameterC { get; }

		// Keeping the same name as in the parent class so it ends up in an overload
		[Export ("encodeToCommandBuffer:sourceMatrices:resultMatrix:scaleVector:offsetVector:biasVector:startIndex:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer buffer, MPSMatrix [] sourceMatrices, MPSMatrix resultMatrix, [NullAllowed] MPSVector scaleVector, [NullAllowed] MPSVector offsetVector, [NullAllowed] MPSVector biasVector, nuint startIndex);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixUnaryKernel))]
	[DisableDefaultCtor]
	interface MPSMatrixSoftMax {

		[Export ("sourceRows")]
		nuint SourceRows { get; set; }

		[Export ("sourceColumns")]
		nuint SourceColumns { get; set; }

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		// Keeping the same name as in the parent class so it ends up in an overload
		[Export ("encodeToCommandBuffer:inputMatrix:resultMatrix:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSMatrix inputMatrix, MPSMatrix resultMatrix);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("copyWithZone:device:")]
		[return: Release ()]
		MPSMatrixSoftMax Copy ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixUnaryKernel))]
	[DisableDefaultCtor]
	interface MPSMatrixNeuron {

		[Export ("sourceNumberOfFeatureVectors")]
		nuint SourceNumberOfFeatureVectors { get; set; }

		[Export ("sourceInputFeatureChannels")]
		nuint SourceInputFeatureChannels { get; set; }

		[Export ("alpha")]
		double Alpha { get; set; }

		[Export ("setNeuronType:parameterA:parameterB:parameterC:")]
		void SetNeuronType (MPSCnnNeuronType neuronType, float parameterA, float parameterB, float parameterC);

		[Export ("neuronType")]
		MPSCnnNeuronType NeuronType { get; }

		[Export ("neuronParameterA")]
		float NeuronParameterA { get; }

		[Export ("neuronParameterB")]
		float NeuronParameterB { get; }

		[Export ("neuronParameterC")]
		float NeuronParameterC { get; }

		[Export ("setNeuronToPReLUWithParametersA:")]
		void SetNeuronToPReLU (NSData parametersA);

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		// Keeping the same name as in the parent class so it ends up in an overload
		[Export ("encodeToCommandBuffer:inputMatrix:biasVector:resultMatrix:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSMatrix inputMatrix, [NullAllowed] MPSVector biasVector, MPSMatrix resultMatrix);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("copyWithZone:device:")]
		[return: Release ()]
		MPSMatrixNeuron Copy ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixBinaryKernel))]
	[DisableDefaultCtor]
	interface MPSMatrixNeuronGradient {

		[Export ("sourceNumberOfFeatureVectors")]
		nuint SourceNumberOfFeatureVectors { get; set; }

		[Export ("sourceInputFeatureChannels")]
		nuint SourceInputFeatureChannels { get; set; }

		[Export ("alpha")]
		double Alpha { get; set; }

		[Export ("setNeuronType:parameterA:parameterB:parameterC:")]
		void SetNeuronType (MPSCnnNeuronType neuronType, float parameterA, float parameterB, float parameterC);

		[Export ("neuronType")]
		MPSCnnNeuronType NeuronType { get; }

		[Export ("neuronParameterA")]
		float NeuronParameterA { get; }

		[Export ("neuronParameterB")]
		float NeuronParameterB { get; }

		[Export ("neuronParameterC")]
		float NeuronParameterC { get; }

		[Export ("setNeuronToPReLUWithParametersA:")]
		void SetNeuronToPReLU (NSData a);

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("encodeToCommandBuffer:gradientMatrix:inputMatrix:biasVector:resultGradientForDataMatrix:resultGradientForBiasVector:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSMatrix gradientMatrix, MPSMatrix inputMatrix, [NullAllowed] MPSVector biasVector, MPSMatrix resultGradientForDataMatrix, [NullAllowed] MPSVector resultGradientForBiasVector);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);

		[Export ("copyWithZone:device:")]
		[return: Release]
		MPSMatrixNeuronGradient Copy ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixBinaryKernel))]
	[DisableDefaultCtor]
	interface MPSMatrixFullyConnectedGradient {

		[Export ("sourceNumberOfFeatureVectors")]
		nuint SourceNumberOfFeatureVectors { get; set; }

		[Export ("sourceOutputFeatureChannels")]
		nuint SourceOutputFeatureChannels { get; set; }

		[Export ("sourceInputFeatureChannels")]
		nuint SourceInputFeatureChannels { get; set; }

		[Export ("alpha")]
		double Alpha { get; set; }

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("encodeGradientForDataToCommandBuffer:gradientMatrix:weightMatrix:resultGradientForDataMatrix:")]
		void EncodeGradientForData (IMTLCommandBuffer commandBuffer, MPSMatrix gradientMatrix, MPSMatrix weightMatrix, MPSMatrix resultGradientForDataMatrix);

		[Export ("encodeGradientForWeightsAndBiasToCommandBuffer:gradientMatrix:inputMatrix:resultGradientForWeightMatrix:resultGradientForBiasVector:")]
		void EncodeGradientForWeightsAndBias (IMTLCommandBuffer commandBuffer, MPSMatrix gradientMatrix, MPSMatrix inputMatrix, MPSMatrix resultGradientForWeightMatrix, [NullAllowed] MPSVector resultGradientForBiasVector);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("copyWithZone:device:")]
		[return: Release]
		MPSMatrixFullyConnectedGradient Copy ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixSoftMax))]
	[DisableDefaultCtor]
	interface MPSMatrixLogSoftMax {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixBinaryKernel))]
	[DisableDefaultCtor]
	interface MPSMatrixFullyConnected {

		[Export ("sourceNumberOfFeatureVectors")]
		nuint SourceNumberOfFeatureVectors { get; set; }

		[Export ("sourceInputFeatureChannels")]
		nuint SourceInputFeatureChannels { get; set; }

		[Export ("sourceOutputFeatureChannels")]
		nuint SourceOutputFeatureChannels { get; set; }

		[Export ("alpha")]
		double Alpha { get; set; }

		[Export ("setNeuronType:parameterA:parameterB:parameterC:")]
		void SetNeuronType (MPSCnnNeuronType neuronType, float parameterA, float parameterB, float parameterC);

		[Export ("neuronType")]
		MPSCnnNeuronType NeuronType { get; }

		[Export ("neuronParameterA")]
		float NeuronParameterA { get; }

		[Export ("neuronParameterB")]
		float NeuronParameterB { get; }

		[Export ("neuronParameterC")]
		float NeuronParameterC { get; }

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		// Keeping the same name as in the parent class so it ends up in an overload
		[Export ("encodeToCommandBuffer:inputMatrix:weightMatrix:biasVector:resultMatrix:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSMatrix inputMatrix, MPSMatrix weightMatrix, [NullAllowed] MPSVector biasVector, MPSMatrix resultMatrix);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("copyWithZone:device:")]
		[return: Release ()]
		MPSMatrixFullyConnected Copy ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixUnaryKernel))]
	[DisableDefaultCtor]
	interface MPSMatrixFindTopK {

		[Export ("sourceRows")]
		nuint SourceRows { get; set; }

		[Export ("sourceColumns")]
		nuint SourceColumns { get; set; }

		[Export ("indexOffset")]
		nuint IndexOffset { get; set; }

		[Export ("numberOfTopKValues")]
		nuint NumberOfTopKValues { get; set; }

		[Export ("initWithDevice:numberOfTopKValues:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint numberOfTopKValues);

		// Keeping the same name as in the parent class so it ends up in an overload
		[Export ("encodeToCommandBuffer:inputMatrix:resultIndexMatrix:resultValueMatrix:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSMatrix inputMatrix, MPSMatrix resultIndexMatrix, MPSMatrix resultValueMatrix);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("copyWithZone:device:")]
		[return: Release ()]
		MPSMatrixFindTopK Copy ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	interface MPSStateResourceList {

		[Static]
		[Export ("resourceList")]
		MPSStateResourceList Create ();

		// The following two variadic selectors are manually bound in MPSStateResourceList.cs
		//[Static, Internal]
		//[Export ("resourceListWithTextureDescriptors:", IsVariadic = true)]
		//MPSStateResourceList ResourceListWithTextureDescriptors (MTLTextureDescriptor descriptor, IntPtr varArgs);

		//[Static, Internal]
		//[Export ("resourceListWithBufferSizes:", IsVariadic = true)]
		//MPSStateResourceList ResourceListWithBufferSizes (nuint firstSize, IntPtr varArgs);

		[Export ("appendTexture:")]
		void Append (MTLTextureDescriptor descriptor);

		[Export ("appendBuffer:")]
		void Append (nuint bufferSize);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSKeyedUnarchiver))]
	[DisableDefaultCtor]
	interface MPSKeyedUnarchiver : MPSDeviceProvider {

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("unarchivedObjectOfClasses:fromData:device:error:")]
		[return: NullAllowed]
		NSObject GetUnarchivedObject (NSSet<Class> classes, NSData data, IMTLDevice device, [NullAllowed] out NSError error);

		[MacCatalyst (13, 1)]
		[Static]
		[Export ("unarchivedObjectOfClass:fromData:device:error:")]
		[return: NullAllowed]
		NSObject GetUnarchivedObject (Class @class, NSData data, IMTLDevice device, [NullAllowed] out NSError error);

		[MacCatalyst (13, 1)]
		[Export ("initForReadingFromData:device:error:")]
		NativeHandle Constructor (NSData data, IMTLDevice device, [NullAllowed] out NSError error);

		// Comes from MPSDeviceProvider
		//[Export ("mpsMTLDevice")]
		//IMTLDevice GetMTLDevice ();

		// According to header docs the following are deprecated and not binary compatibile,
		// so there is not much point in binding/using these, they only short lived
		// for iOS 11.3 and Mac 10.13.4 and were considered experimental.

		//
		//[Deprecated (PlatformName.TvOS, 12, 0)]
		//[Deprecated (PlatformName.iOS, 12, 0)]
		//[Deprecated (PlatformName.MacOSX, 10, 14)]
		//[Static]
		//[Export ("unarchiveObjectWithData:device:")]
		//[return: NullAllowed]
		//NSObject UnarchiveObjectWithData (NSData data, IMTLDevice device);

		//
		//[Deprecated (PlatformName.TvOS, 12, 0)]
		//[Deprecated (PlatformName.iOS, 12, 0)]
		//[Deprecated (PlatformName.MacOSX, 10, 14)]
		//[Static]
		//[Export ("unarchiveTopLevelObjectWithData:device:error:")]
		//[return: NullAllowed]
		//NSObject UnarchiveTopLevelObjectWithData (NSData data, IMTLDevice device, [NullAllowed] out NSError error);

		//
		//[Deprecated (PlatformName.TvOS, 12, 0)]
		//[Deprecated (PlatformName.iOS, 12, 0)]
		//[Deprecated (PlatformName.MacOSX, 10, 14)]
		//[Static]
		//[Export ("unarchiveObjectWithFile:device:")]
		//[return: NullAllowed]
		//NSObject UnarchiveObjectWithFile (string path, IMTLDevice device);

		//
		//[Deprecated (PlatformName.TvOS, 12, 0)]
		//[Deprecated (PlatformName.iOS, 12, 0)]
		//[Deprecated (PlatformName.MacOSX, 10, 14)]
		//[Export ("initWithDevice:")]
		//NativeHandle Constructor (IMTLDevice device);

		//
		//[Deprecated (PlatformName.TvOS, 12, 0)]
		//[Deprecated (PlatformName.iOS, 12, 0)]
		//[Deprecated (PlatformName.MacOSX, 10, 14)]
		//[Export ("initForReadingWithData:device:")]
		//NativeHandle Constructor (NSData data, IMTLDevice device);
	}

	[TV (13, 0), iOS (13, 0)]
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MPSNDArrayDescriptor {
		[Export ("dataType", ArgumentSemantic.Assign)]
		MPSDataType DataType { get; set; }

		[Export ("numberOfDimensions")]
		nuint NumberOfDimensions { get; set; }

		[Export ("lengthOfDimension:")]
		nuint GetLength (nuint dimensionIndex);

		[Export ("sliceRangeForDimension:")]
		MPSDimensionSlice GetSliceRange (nuint dimensionIndex);

		[Export ("sliceDimension:withSubrange:")]
		void Slice (nuint dimensionIndex, MPSDimensionSlice subRange);

		[Export ("transposeDimension:withDimension:")]
		void Transpose (nuint dimensionIndex, nuint dimensionIndex2);

		// -(vector_uchar16)dimensionOrder;
		// [Export ("dimensionOrder")]
		// [Verify (MethodToProperty)]
		// [unsupported ExtVector: unsigned char __attribute__((ext_vector_type(16)))] DimensionOrder { get; }

		[Static]
		[Export ("descriptorWithDataType:dimensionCount:dimensionSizes:")]
		MPSNDArrayDescriptor Create (MPSDataType dataType, nuint numberOfDimensions, IntPtr dimensionSizes);

		[Static]
		[Export ("descriptorWithDataType:shape:")]
		MPSNDArrayDescriptor Create (MPSDataType dataType, [BindAs (typeof (nuint []))] NSNumber [] shape);

		// This is implemented using params and descriptorWithDataType:dimensionCount:dimensionSizes: in MPSNDArray.cs
		// [Static, Internal]
		// [Export ("descriptorWithDataType:dimensionSizes:", IsVariadic = true)]
		// MPSNDArrayDescriptor Create (MPSDataType dataType, nuint dimension0, IntPtr varArgs);

		[Export ("reshapeWithDimensionCount:dimensionSizes:")]
		void Reshape (nuint numberOfDimensions, IntPtr dimensionSizes);

		[Export ("reshapeWithShape:")]
		void Reshape ([BindAs (typeof (nuint []))] NSNumber [] shape);

		[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
		[Export ("preferPackedRows")]
		bool PreferPackedRows { get; set; }

		[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
		[Export ("permuteWithDimensionOrder:")]
		[Internal]
		void _PermuteWithDimensionOrder (/* NSUInteger* */ IntPtr dimensionOrder);

		[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
		[Export ("getShape")]
		[return: BindAs (typeof (nuint []))]
		NSNumber [] GetShape ();
	}

	interface IMPSNDArrayAllocator { }

	[Protocol]
	interface MPSNDArrayAllocator : NSCoding, NSCopying, NSSecureCoding {
		[Abstract]
		[Export ("arrayForCommandBuffer:arrayDescriptor:kernel:")]
		MPSNDArray AllocateArray (IMTLCommandBuffer cmdBuf, MPSNDArrayDescriptor descriptor, MPSKernel kernel);
	}

	[TV (13, 0), iOS (13, 0)]
	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MPSNDArray {
		[Static]
		[Export ("defaultAllocator")]
		IMPSNDArrayAllocator DefaultAllocator { get; }

		[NullAllowed, Export ("label")]
		string Label { get; set; }

		[Export ("dataType")]
		MPSDataType DataType { get; }

		[Export ("dataTypeSize")]
		nuint DataTypeSize { get; }

		[Export ("numberOfDimensions")]
		nuint NumberOfDimensions { get; }

		[Export ("lengthOfDimension:")]
		nuint GetLength (nuint dimensionIndex);

		[Export ("device", ArgumentSemantic.Retain)]
		IMTLDevice Device { get; }

		[Export ("descriptor")]
		MPSNDArrayDescriptor Descriptor { get; }

		[Export ("initWithDevice:descriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSNDArrayDescriptor descriptor);

		[Export ("initWithDevice:scalar:")]
		NativeHandle Constructor (IMTLDevice device, double value);

		[Export ("resourceSize")]
		nuint ResourceSize { get; }

		[Export ("arrayViewWithCommandBuffer:descriptor:aliasing:")]
		[return: NullAllowed]
		MPSNDArray Create (IMTLCommandBuffer cmdBuf, MPSNDArrayDescriptor descriptor, MPSAliasingStrategy aliasing);

		[NullAllowed, Export ("parent", ArgumentSemantic.Retain)]
		MPSNDArray Parent { get; }

		[Export ("exportDataWithCommandBuffer:toBuffer:destinationDataType:offset:rowStrides:")]
		void ExportData (IMTLCommandBuffer cmdBuf, IMTLBuffer buffer, MPSDataType destinationDataType, nuint offset, IntPtr rowStrides);

		[Export ("importDataWithCommandBuffer:fromBuffer:sourceDataType:offset:rowStrides:")]
		void ImportData (IMTLCommandBuffer cmdBuf, IMTLBuffer buffer, MPSDataType sourceDataType, nuint offset, IntPtr rowStrides);

		[Export ("exportDataWithCommandBuffer:toImages:offset:")]
		void ExportData (IMTLCommandBuffer cmdBuf, MPSImage [] images, MPSImageCoordinate offset);

		[Export ("importDataWithCommandBuffer:fromImages:offset:")]
		void ImportData (IMTLCommandBuffer cmdBuf, MPSImage [] images, MPSImageCoordinate offset);

		[Export ("readBytes:strideBytes:")]
		void ReadBytes (IntPtr buffer, IntPtr strideBytesPerDimension);

		[Export ("writeBytes:strideBytes:")]
		void WriteBytes (IntPtr buffer, IntPtr strideBytesPerDimension);

		[Export ("synchronizeOnCommandBuffer:")]
		void Synchronize (IMTLCommandBuffer commandBuffer);

		[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
		[Export ("initWithBuffer:offset:descriptor:")]
		NativeHandle Constructor (IMTLBuffer buffer, nuint offset, MPSNDArrayDescriptor descriptor);

		[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
		[NullAllowed]
		[Export ("userBuffer")]
		IMTLBuffer UserBuffer { get; }

		[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
		[Export ("arrayViewWithDescriptor:")]
		[return: NullAllowed]
		MPSNDArray Create (MPSNDArrayDescriptor descriptor);

		[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
		[Export ("arrayViewWithShape:strides:")]
		[return: NullAllowed]
		MPSNDArray Create ([NullAllowed][BindAs (typeof (nint []))] NSNumber [] shape, [BindAs (typeof (nint []))] NSNumber [] strides);

		[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
		[Export ("arrayViewWithDimensionCount:dimensionSizes:strides:")]
		[return: NullAllowed]
		[Internal]
		MPSNDArray _Create (nuint numberOfDimensions, IntPtr dimensionSizes, IntPtr dimStrides);
	}

	[DisableDefaultCtor]
	[TV (13, 0), iOS (13, 0)]
	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNDArray))]
	interface MPSTemporaryNDArray {
		[Static]
		[Export ("defaultAllocator")]
		IMPSNDArrayAllocator DefaultAllocator { get; }

		[Static]
		[Export ("temporaryNDArrayWithCommandBuffer:descriptor:")]
		MPSTemporaryNDArray Create (IMTLCommandBuffer commandBuffer, MPSNDArrayDescriptor descriptor);

		[Export ("readCount")]
		nuint ReadCount { get; set; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImagePyramid))]
	[DisableDefaultCtor]
	interface MPSImageLaplacianPyramid {

		// .ctors inlined from parent class

		[Export ("initWithCoder:device:")]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("initWithDevice:")]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithDevice:centerWeight:")]
		NativeHandle Constructor (IMTLDevice device, float centerWeight);

		[Export ("initWithDevice:kernelWidth:kernelHeight:weights:")]
		[Internal]
		IntPtr InitWithDevice (IMTLDevice device, nuint kernelWidth, nuint kernelHeight, /* float* */ IntPtr kernelWeights);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("laplacianBias")]
		float LaplacianBias { [Bind ("getLaplacianBias")] get; [Bind ("setLaplacianBias:")] set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("laplacianScale")]
		float LaplacianScale { [Bind ("getLaplacianScale")] get; [Bind ("setLaplacianScale:")] set; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageLaplacianPyramid))]
	[DisableDefaultCtor]
	interface MPSImageLaplacianPyramidSubtract {
		// .ctors inlined from parent class

		[Export ("initWithCoder:device:")]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("initWithDevice:")]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithDevice:centerWeight:")]
		NativeHandle Constructor (IMTLDevice device, float centerWeight);

		[Export ("initWithDevice:kernelWidth:kernelHeight:weights:")]
		[Internal]
		IntPtr InitWithDevice (IMTLDevice device, nuint kernelWidth, nuint kernelHeight, /* float* */ IntPtr kernelWeights);
	}


	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageLaplacianPyramid))]
	[DisableDefaultCtor]
	interface MPSImageLaplacianPyramidAdd {
		// .ctors inlined from parent class

		[Export ("initWithCoder:device:")]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("initWithDevice:")]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithDevice:centerWeight:")]
		NativeHandle Constructor (IMTLDevice device, float centerWeight);

		[Export ("initWithDevice:kernelWidth:kernelHeight:weights:")]
		[Internal]
		IntPtr InitWithDevice (IMTLDevice device, nuint kernelWidth, nuint kernelHeight, /* float* */ IntPtr kernelWeights);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel))]
	[DisableDefaultCtor]
	interface MPSMatrixCopyToImage {

		[Export ("sourceMatrixOrigin", ArgumentSemantic.Assign)]
		MTLOrigin SourceMatrixOrigin { get; set; }

		[Export ("sourceMatrixBatchIndex")]
		nuint SourceMatrixBatchIndex { get; set; }

		[Export ("dataLayout")]
		MPSDataLayout DataLayout { get; }

		[Export ("initWithDevice:dataLayout:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSDataLayout dataLayout);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("encodeToCommandBuffer:sourceMatrix:destinationImage:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSMatrix sourceMatrix, MPSImage destinationImage);

		[Export ("encodeBatchToCommandBuffer:sourceMatrix:destinationImages:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSMatrix sourceMatrix, NSArray<MPSImage> destinationImages);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor]
	interface MPSImageEuclideanDistanceTransform {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel))]
	[DisableDefaultCtor]
	interface MPSImageGuidedFilter {

		[Export ("kernelDiameter")]
		nuint KernelDiameter { get; }

		[Export ("epsilon")]
		float Epsilon { get; set; }

		[Export ("reconstructScale")]
		float ReconstructScale { get; set; }

		[Export ("reconstructOffset")]
		float ReconstructOffset { get; set; }

		[Export ("initWithDevice:kernelDiameter:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint kernelDiameter);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("encodeRegressionToCommandBuffer:sourceTexture:guidanceTexture:weightsTexture:destinationCoefficientsTexture:")]
		void EncodeRegression (IMTLCommandBuffer commandBuffer, IMTLTexture sourceTexture, IMTLTexture guidanceTexture, [NullAllowed] IMTLTexture weightsTexture, IMTLTexture destinationCoefficientsTexture);

		[Export ("encodeReconstructionToCommandBuffer:guidanceTexture:coefficientsTexture:destinationTexture:")]
		void EncodeReconstruction (IMTLCommandBuffer commandBuffer, IMTLTexture guidanceTexture, IMTLTexture coefficientsTexture, IMTLTexture destinationTexture);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel))]
	[DisableDefaultCtor]
	interface MPSImageNormalizedHistogram {

		[Export ("clipRectSource", ArgumentSemantic.Assign)]
		MTLRegion ClipRectSource { get; set; }

		[Export ("zeroHistogram")]
		bool ZeroHistogram { get; set; }

		[Export ("histogramInfo")]
		MPSImageHistogramInfo HistogramInfo {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			get;
		}

		[Export ("initWithDevice:histogramInfo:")]
		[DesignatedInitializer]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		NativeHandle Constructor (IMTLDevice device, ref MPSImageHistogramInfo histogramInfo);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("encodeToCommandBuffer:sourceTexture:minmaxTexture:histogram:histogramOffset:")]
		void Encode (IMTLCommandBuffer commandBuffer, IMTLTexture source, IMTLTexture minmaxTexture, IMTLBuffer histogram, nuint histogramOffset);

		[Export ("histogramSizeForSourceFormat:")]
		nuint GetHistogramSize (MTLPixelFormat sourceFormat);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSUnaryImageKernel))]
	[DisableDefaultCtor] // Only subclasses are meant to be used
	interface MPSImageReduceUnary {

		[Export ("clipRectSource", ArgumentSemantic.Assign)]
		MTLRegion ClipRectSource { get; set; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageReduceUnary))]
	[DisableDefaultCtor]
	interface MPSImageReduceRowMin {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageReduceUnary))]
	[DisableDefaultCtor]
	interface MPSImageReduceColumnMin {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageReduceUnary))]
	[DisableDefaultCtor]
	interface MPSImageReduceRowMax {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageReduceUnary))]
	[DisableDefaultCtor]
	interface MPSImageReduceColumnMax {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageReduceUnary))]
	[DisableDefaultCtor]
	interface MPSImageReduceRowMean {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageReduceUnary))]
	[DisableDefaultCtor]
	interface MPSImageReduceColumnMean {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageReduceUnary))]
	[DisableDefaultCtor]
	interface MPSImageReduceRowSum {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSImageReduceUnary))]
	[DisableDefaultCtor]
	interface MPSImageReduceColumnSum {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixBinaryKernel))]
	[DisableDefaultCtor]
	interface MPSMatrixSoftMaxGradient {

		[Export ("sourceRows")]
		nuint SourceRows { get; set; }

		[Export ("sourceColumns")]
		nuint SourceColumns { get; set; }

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		// Keeping the same name as in the parent class so it ends up in an overload
		[Export ("encodeToCommandBuffer:gradientMatrix:forwardOutputMatrix:resultMatrix:")]
		void EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSMatrix gradientMatrix, MPSMatrix forwardOutputMatrix, MPSMatrix resultMatrix);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("copyWithZone:device:")]
		[return: Release]
		MPSMatrixSoftMaxGradient Copy ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixSoftMaxGradient))]
	[DisableDefaultCtor]
	interface MPSMatrixLogSoftMaxGradient {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel))]
	[DisableDefaultCtor]
	interface MPSRayIntersector : NSSecureCoding, NSCopying {

		[Export ("cullMode", ArgumentSemantic.Assign)]
		MTLCullMode CullMode { get; set; }

		[Export ("frontFacingWinding", ArgumentSemantic.Assign)]
		MTLWinding FrontFacingWinding { get; set; }

		[Export ("triangleIntersectionTestType", ArgumentSemantic.Assign)]
		MPSTriangleIntersectionTestType TriangleIntersectionTestType { get; set; }

		[Export ("boundingBoxIntersectionTestType", ArgumentSemantic.Assign)]
		MPSBoundingBoxIntersectionTestType BoundingBoxIntersectionTestType { get; set; }

		[Export ("rayMaskOptions", ArgumentSemantic.Assign)]
		MPSRayMaskOptions RayMaskOptions { get; set; }

		[Export ("rayStride")]
		nuint RayStride { get; set; }

		[Export ("intersectionStride")]
		nuint IntersectionStride { get; set; }

		[Export ("rayDataType", ArgumentSemantic.Assign)]
		MPSRayDataType RayDataType { get; set; }

		[Export ("intersectionDataType", ArgumentSemantic.Assign)]
		MPSIntersectionDataType IntersectionDataType { get; set; }

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("copyWithZone:device:")]
		[return: Release]
		MPSRayIntersector Copy ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);

		[Export ("recommendedMinimumRayBatchSizeForRayCount:")]
		nuint GetRecommendedMinimumRayBatchSize (nuint rayCount);

		[Export ("encodeWithCoder:")]
		void Encode (NSCoder coder);

		[Export ("encodeIntersectionToCommandBuffer:intersectionType:rayBuffer:rayBufferOffset:intersectionBuffer:intersectionBufferOffset:rayCount:accelerationStructure:")]
		void EncodeIntersection (IMTLCommandBuffer commandBuffer, MPSIntersectionType intersectionType, IMTLBuffer rayBuffer, nuint rayBufferOffset, IMTLBuffer intersectionBuffer, nuint intersectionBufferOffset, nuint rayCount, MPSAccelerationStructure accelerationStructure);

		[Export ("encodeIntersectionToCommandBuffer:intersectionType:rayBuffer:rayBufferOffset:intersectionBuffer:intersectionBufferOffset:rayCountBuffer:rayCountBufferOffset:accelerationStructure:")]
		void EncodeIntersection (IMTLCommandBuffer commandBuffer, MPSIntersectionType intersectionType, IMTLBuffer rayBuffer, nuint rayBufferOffset, IMTLBuffer intersectionBuffer, nuint intersectionBufferOffset, IMTLBuffer rayCountBuffer, nuint rayCountBufferOffset, MPSAccelerationStructure accelerationStructure);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MPSAccelerationStructureGroup {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("device")]
		IMTLDevice Device { get; }

		[Export ("initWithDevice:")]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSAccelerationStructure))]
	[DisableDefaultCtor]
	interface MPSInstanceAccelerationStructure {

		[NullAllowed, Export ("accelerationStructures", ArgumentSemantic.Retain)]
		MPSTriangleAccelerationStructure [] AccelerationStructures { get; set; }

		[NullAllowed, Export ("instanceBuffer", ArgumentSemantic.Retain)]
		IMTLBuffer InstanceBuffer { get; set; }

		[Export ("instanceBufferOffset")]
		nuint InstanceBufferOffset { get; set; }

		[NullAllowed, Export ("transformBuffer", ArgumentSemantic.Retain)]
		IMTLBuffer TransformBuffer { get; set; }

		[Export ("transformBufferOffset")]
		nuint TransformBufferOffset { get; set; }

		[Export ("transformType", ArgumentSemantic.Assign)]
		MPSTransformType TransformType { get; set; }

		[NullAllowed, Export ("maskBuffer", ArgumentSemantic.Retain)]
		IMTLBuffer MaskBuffer { get; set; }

		[Export ("maskBufferOffset")]
		nuint MaskBufferOffset { get; set; }

		[Export ("instanceCount")]
		nuint InstanceCount { get; set; }

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("initWithGroup:")]
		[DesignatedInitializer]
		NativeHandle Constructor (MPSAccelerationStructureGroup group);

		[Export ("initWithCoder:group:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, MPSAccelerationStructureGroup group);
	}

	delegate void MPSAccelerationStructureCompletionHandler ([NullAllowed] MPSAccelerationStructure structure);

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel))]
	[DisableDefaultCtor]
	interface MPSAccelerationStructure : NSSecureCoding, NSCopying {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("group")]
		MPSAccelerationStructureGroup Group { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("boundingBox")]
		MPSAxisAlignedBoundingBox BoundingBox {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			get;
		}

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("status")]
		MPSAccelerationStructureStatus Status { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("usage", ArgumentSemantic.Assign)]
		MPSAccelerationStructureUsage Usage { get; set; }

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("initWithGroup:")]
		[DesignatedInitializer]
		NativeHandle Constructor (MPSAccelerationStructureGroup group);

		[Export ("initWithCoder:group:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, MPSAccelerationStructureGroup group);

		[Export ("rebuild")]
		void Rebuild ();

		[Async]
		[Export ("rebuildWithCompletionHandler:")]
		void Rebuild (MPSAccelerationStructureCompletionHandler completionHandler);

		[Export ("encodeRefitToCommandBuffer:")]
		void EncodeRefit (IMTLCommandBuffer commandBuffer);

		[Export ("copyWithZone:device:")]
		[return: Release]
		MPSAccelerationStructure Copy ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);

		[Export ("copyWithZone:group:")]
		[return: Release]
		MPSAccelerationStructure Copy ([NullAllowed] NSZone zone, MPSAccelerationStructureGroup group);

		[Export ("encodeWithCoder:")]
		void Encode (NSCoder coder);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSAccelerationStructure))]
	[DisableDefaultCtor]
	interface MPSTriangleAccelerationStructure {

		[NullAllowed, Export ("vertexBuffer", ArgumentSemantic.Retain)]
		IMTLBuffer VertexBuffer { get; set; }

		[Export ("vertexBufferOffset")]
		nuint VertexBufferOffset { get; set; }

		[Export ("vertexStride")]
		nuint VertexStride { get; set; }

		[NullAllowed, Export ("indexBuffer", ArgumentSemantic.Retain)]
		IMTLBuffer IndexBuffer { get; set; }

		[Export ("indexType", ArgumentSemantic.Assign)]
		MPSDataType IndexType { get; set; }

		[Export ("indexBufferOffset")]
		nuint IndexBufferOffset { get; set; }

		[NullAllowed, Export ("maskBuffer", ArgumentSemantic.Retain)]
		IMTLBuffer MaskBuffer { get; set; }

		[Export ("maskBufferOffset")]
		nuint MaskBufferOffset { get; set; }

		[Export ("triangleCount")]
		nuint TriangleCount { get; set; }

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("initWithGroup:")]
		[DesignatedInitializer]
		NativeHandle Constructor (MPSAccelerationStructureGroup group);

		[Export ("initWithCoder:group:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, MPSAccelerationStructureGroup group);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientState), Name = "MPSCNNBatchNormalizationState")]
	[DisableDefaultCtor]
	interface MPSCnnBatchNormalizationState {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("batchNormalization", ArgumentSemantic.Retain)]
		MPSCnnBatchNormalization BatchNormalization { get; }

		[Export ("reset")]
		void Reset ();

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NullAllowed, Export ("gamma")]
		IMTLBuffer Gamma { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NullAllowed, Export ("beta")]
		IMTLBuffer Beta { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NullAllowed, Export ("mean")]
		IMTLBuffer Mean { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NullAllowed, Export ("variance")]
		IMTLBuffer Variance { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NullAllowed, Export ("gradientForGamma")]
		IMTLBuffer GradientForGamma { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NullAllowed, Export ("gradientForBeta")]
		IMTLBuffer GradientForBeta { get; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSState), Name = "MPSCNNNormalizationMeanAndVarianceState")]
	[DisableDefaultCtor]
	interface MPSCnnNormalizationMeanAndVarianceState {

		[Export ("mean")]
		IMTLBuffer Mean { get; }

		[Export ("variance")]
		IMTLBuffer Variance { get; }

		[Export ("initWithMean:variance:")]
		NativeHandle Constructor (IMTLBuffer mean, IMTLBuffer variance);

		[Static]
		[Export ("temporaryStateWithCommandBuffer:numberOfFeatureChannels:")]
		MPSCnnNormalizationMeanAndVarianceState GetTemporaryState (IMTLCommandBuffer commandBuffer, nuint numberOfFeatureChannels);
	}

	interface IMPSCnnBatchNormalizationDataSource { }

	[MacCatalyst (13, 1)]
#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject), Name = "MPSCNNBatchNormalizationDataSource")]
	interface MPSCnnBatchNormalizationDataSource : NSCopying {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Abstract]
		[Export ("numberOfFeatureChannels")]
		nuint NumberOfFeatureChannels { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Abstract]
		[Export ("gamma")]
		IntPtr /* float* */ Gamma { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Abstract]
		[Export ("beta")]
		IntPtr /* float* */ Beta { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Abstract]
		[Export ("mean")]
		IntPtr /* float* */ Mean { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Abstract]
		[Export ("variance")]
		IntPtr /* float* */ Variance { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Abstract]
		[Export ("load")]
		bool Load { get; }

		[Abstract]
		[Export ("purge")]
		void Purge ();

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Abstract]
		[NullAllowed, Export ("label")]
		string Label { get; }

		[Export ("updateGammaAndBetaWithCommandBuffer:batchNormalizationState:")]
		[return: NullAllowed]
		MPSCnnNormalizationGammaAndBetaState UpdateGammaAndBeta (IMTLCommandBuffer commandBuffer, MPSCnnBatchNormalizationState batchNormalizationState);

		[MacCatalyst (13, 1)]
		[Export ("updateMeanAndVarianceWithCommandBuffer:batchNormalizationState:")]
		[return: NullAllowed]
		MPSCnnNormalizationMeanAndVarianceState UpdateMeanAndVariance (IMTLCommandBuffer commandBuffer, MPSCnnBatchNormalizationState batchNormalizationState);

		[Export ("updateGammaAndBetaWithBatchNormalizationState:")]
		bool UpdateGammaAndBeta (MPSCnnBatchNormalizationState batchNormalizationState);

		[MacCatalyst (13, 1)]
		[Export ("updateMeanAndVarianceWithBatchNormalizationState:")]
		bool UpdateMeanAndVariance (MPSCnnBatchNormalizationState batchNormalizationState);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("epsilon")]
		float Epsilon { get; }

		[Export ("encodeWithCoder:")]
		void Encode (NSCoder coder);

		[Export ("initWithCoder:")]
		NativeHandle Constructor (NSCoder decoder);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Static]
		[Export ("supportsSecureCoding")]
		bool SupportsSecureCoding { get; }

		[MacCatalyst (13, 1)]
		[Export ("copyWithZone:device:")]
		[return: Release]
		IMPSCnnBatchNormalizationDataSource Copy ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel), Name = "MPSCNNBatchNormalization")]
	[DisableDefaultCtor]
	interface MPSCnnBatchNormalization {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("numberOfFeatureChannels")]
		nuint NumberOfFeatureChannels { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("epsilon")]
		float Epsilon { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("dataSource", ArgumentSemantic.Retain)]
		IMPSCnnBatchNormalizationDataSource DataSource { get; }

		[Export ("initWithDevice:dataSource:")]
		NativeHandle Constructor (IMTLDevice device, IMPSCnnBatchNormalizationDataSource dataSource);

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:dataSource:fusedNeuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, IMPSCnnBatchNormalizationDataSource dataSource, [NullAllowed] MPSNNNeuronDescriptor fusedNeuronDescriptor);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);

		[Export ("encodeToCommandBuffer:sourceImage:batchNormalizationState:destinationImage:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSImage sourceImage, MPSCnnBatchNormalizationState batchNormalizationState, MPSImage destinationImage);

		[Export ("encodeBatchToCommandBuffer:sourceImages:batchNormalizationState:destinationImages:")]
		void EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceImages, MPSCnnBatchNormalizationState batchNormalizationState, NSArray<MPSImage> destinationImages);

		[Export ("resultStateForSourceImage:sourceStates:destinationImage:")]
		[return: NullAllowed]
		MPSCnnBatchNormalizationState GetResultState (MPSImage sourceImage, [NullAllowed] NSArray<MPSState> sourceStates, MPSImage destinationImage);

		[Export ("temporaryResultStateForCommandBuffer:sourceImage:sourceStates:destinationImage:")]
		[return: NullAllowed]
		MPSCnnBatchNormalizationState GetTemporaryResultState (IMTLCommandBuffer commandBuffer, MPSImage sourceImage, [NullAllowed] NSArray<MPSState> sourceStates, MPSImage destinationImage);

		//[Deprecated (PlatformName.TvOS, 12, 0, message: "Please use 'ReloadGammaAndBetaFromDataSource' and/or 'ReloadMeanAndVarianceFromDataSource' instead.")]
		//[Deprecated (PlatformName.iOS, 12, 0, message: "Please use 'ReloadGammaAndBetaFromDataSource' and/or 'ReloadMeanAndVarianceFromDataSource' instead.")]
		//[Deprecated (PlatformName.MacOSX, 10, 14, message: "Please use 'ReloadGammaAndBetaFromDataSource' and/or 'ReloadMeanAndVarianceFromDataSource' instead.")]
		//[Export ("reloadDataSource:")]
		//void ReloadDataSource (IMPSCnnBatchNormalizationDataSource dataSource);

		[MacCatalyst (13, 1)]
		[Export ("reloadGammaAndBetaFromDataSource")]
		void ReloadGammaAndBetaFromDataSource ();

		[MacCatalyst (13, 1)]
		[Export ("reloadMeanAndVarianceFromDataSource")]
		void ReloadMeanAndVarianceFromDataSource ();

		[Export ("reloadGammaAndBetaWithCommandBuffer:gammaAndBetaState:")]
		void ReloadGammaAndBeta (IMTLCommandBuffer commandBuffer, MPSCnnNormalizationGammaAndBetaState gammaAndBetaState);

		[MacCatalyst (13, 1)]
		[Export ("reloadMeanAndVarianceWithCommandBuffer:meanAndVarianceState:")]
		void ReloadMeanAndVariance (IMTLCommandBuffer commandBuffer, MPSCnnNormalizationMeanAndVarianceState meanAndVarianceState);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel), Name = "MPSCNNBatchNormalizationStatistics")]
	[DisableDefaultCtor]
	interface MPSCnnBatchNormalizationStatistics {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);

		[Export ("encodeBatchToCommandBuffer:sourceImages:batchNormalizationState:")]
		void EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceImages, MPSCnnBatchNormalizationState batchNormalizationState);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnGradientKernel), Name = "MPSCNNBatchNormalizationGradient")]
	[DisableDefaultCtor]
	interface MPSCnnBatchNormalizationGradient {

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:fusedNeuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, [NullAllowed] MPSNNNeuronDescriptor fusedNeuronDescriptor);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);

		[Export ("encodeToCommandBuffer:sourceGradient:sourceImage:batchNormalizationState:destinationGradient:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSImage sourceGradient, MPSImage sourceImage, MPSCnnBatchNormalizationState batchNormalizationState, MPSImage destinationGradient);

		[Export ("encodeBatchToCommandBuffer:sourceGradients:sourceImages:batchNormalizationState:destinationGradients:")]
		void EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceGradients, NSArray<MPSImage> sourceImages, MPSCnnBatchNormalizationState batchNormalizationState, NSArray<MPSImage> destinationGradients);

		[Export ("encodeToCommandBuffer:sourceGradient:sourceImage:batchNormalizationState:")]
		MPSImage Encode (IMTLCommandBuffer commandBuffer, MPSImage sourceGradient, MPSImage sourceImage, MPSCnnBatchNormalizationState batchNormalizationState);

		[Export ("encodeBatchToCommandBuffer:sourceGradients:sourceImages:batchNormalizationState:")]
		NSArray<MPSImage> EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceGradients, NSArray<MPSImage> sourceImages, MPSCnnBatchNormalizationState batchNormalizationState);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnGradientKernel), Name = "MPSCNNBatchNormalizationStatisticsGradient")]
	[DisableDefaultCtor]
	interface MPSCnnBatchNormalizationStatisticsGradient {

		[MacCatalyst (13, 1)]
		[Export ("initWithDevice:fusedNeuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, [NullAllowed] MPSNNNeuronDescriptor fusedNeuronDescriptor);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("encodeBatchToCommandBuffer:sourceGradients:sourceImages:batchNormalizationState:")]
		void EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceGradients, NSArray<MPSImage> sourceImages, MPSCnnBatchNormalizationState batchNormalizationState);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientState), Name = "MPSCNNConvolutionGradientState")]
	[DisableDefaultCtor]
	interface MPSCnnConvolutionGradientState : MPSImageSizeEncodingState {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("gradientForWeights")]
		IMTLBuffer GradientForWeights { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("gradientForBiases")]
		IMTLBuffer GradientForBiases { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("convolution", ArgumentSemantic.Retain)]
		MPSCnnConvolution Convolution { get; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSState), Name = "MPSCNNConvolutionWeightsAndBiasesState")]
	[DisableDefaultCtor]
	interface MPSCnnConvolutionWeightsAndBiasesState {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("weights")]
		IMTLBuffer Weights { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NullAllowed, Export ("biases")]
		IMTLBuffer Biases { get; }

		[Export ("initWithWeights:biases:")]
		NativeHandle Constructor (IMTLBuffer weights, [NullAllowed] IMTLBuffer biases);

		[Export ("initWithDevice:cnnConvolutionDescriptor:")]
		NativeHandle Constructor (IMTLDevice device, MPSCnnConvolutionDescriptor descriptor);

		[Static]
		[Export ("temporaryCNNConvolutionWeightsAndBiasesStateWithCommandBuffer:cnnConvolutionDescriptor:")]
		MPSCnnConvolutionWeightsAndBiasesState GetTemporaryCnnConvolutionWeightsAndBiasesState (IMTLCommandBuffer commandBuffer, MPSCnnConvolutionDescriptor descriptor);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnGradientKernel), Name = "MPSCNNConvolutionGradient")]
	[DisableDefaultCtor]
	interface MPSCnnConvolutionGradient {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("sourceGradientFeatureChannels")]
		nuint SourceGradientFeatureChannels { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("sourceImageFeatureChannels")]
		nuint SourceImageFeatureChannels { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("groups")]
		nuint Groups { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("channelMultiplier")]
		nuint ChannelMultiplier { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("dataSource", ArgumentSemantic.Retain)]
		IMPSCnnConvolutionDataSource DataSource { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("gradientOption", ArgumentSemantic.Assign)]
		MPSCnnConvolutionGradientOption GradientOption { get; set; }

		//[Deprecated (PlatformName.TvOS, 12, 0, message: "This doesn't do anything. It is here for backward compatibility. 'MPSCnnConvolutionGradient doesn't serialize weights. It gets weight from 'State.Convolution.DataSource' on first use i.e. first 'Encode' call.")]
		//[Deprecated (PlatformName.iOS, 12, 0, message: "This doesn't do anything. It is here for backward compatibility. 'MPSCnnConvolutionGradient doesn't serialize weights. It gets weight from 'State.Convolution.DataSource' on first use i.e. first 'Encode' call.")]
		//[Deprecated (PlatformName.MacOSX, 10, 14, message: "This doesn't do anything. It is here for backward compatibility. 'MPSCnnConvolutionGradient doesn't serialize weights. It gets weight from 'State.Convolution.DataSource' on first use i.e. first 'Encode' call.")]
		//[Export ("serializeWeightsAndBiases")]
		//bool SerializeWeightsAndBiases { get; set; }

		[Export ("initWithDevice:weights:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, IMPSCnnConvolutionDataSource weights);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);

		[MacCatalyst (13, 1)]
		[Export ("reloadWeightsAndBiasesFromDataSource")]
		void ReloadWeightsAndBiasesFromDataSource ();

		[Export ("reloadWeightsAndBiasesWithCommandBuffer:state:")]
		void ReloadWeightsAndBiases (IMTLCommandBuffer commandBuffer, MPSCnnConvolutionWeightsAndBiasesState state);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnConvolutionGradient), Name = "MPSCNNFullyConnectedGradient")]
	[DisableDefaultCtor]
	interface MPSCnnFullyConnectedGradient {

		[Export ("initWithDevice:weights:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, IMPSCnnConvolutionDataSource weights);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientState), Name = "MPSCNNDropoutGradientState")]
	[DisableDefaultCtor]
	interface MPSCnnDropoutGradientState {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("maskData")]
		NSData MaskData { get; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel), Name = "MPSCNNDropout")]
	[DisableDefaultCtor]
	interface MPSCnnDropout {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("keepProbability")]
		float KeepProbability { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("seed")]
		nuint Seed { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("maskStrideInPixels")]
		MTLSize MaskStrideInPixels { get; }

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);

		[Export ("initWithDevice:keepProbability:seed:maskStrideInPixels:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, float keepProbability, nuint seed, MTLSize maskStrideInPixels);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnGradientKernel), Name = "MPSCNNDropoutGradient")]
	[DisableDefaultCtor]
	interface MPSCnnDropoutGradient {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("keepProbability")]
		float KeepProbability { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("seed")]
		nuint Seed { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("maskStrideInPixels")]
		MTLSize MaskStrideInPixels { get; }

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);

		[Export ("initWithDevice:keepProbability:seed:maskStrideInPixels:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, float keepProbability, nuint seed, MTLSize maskStrideInPixels);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNGradientState), Name = "MPSCNNInstanceNormalizationGradientState")]
	[DisableDefaultCtor]
	interface MPSCnnInstanceNormalizationGradientState {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("instanceNormalization", ArgumentSemantic.Retain)]
		MPSCnnInstanceNormalization InstanceNormalization { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NullAllowed, Export ("gamma")]
		IMTLBuffer Gamma { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NullAllowed, Export ("beta")]
		IMTLBuffer Beta { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("gradientForGamma")]
		IMTLBuffer GradientForGamma { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("gradientForBeta")]
		IMTLBuffer GradientForBeta { get; }
	}

	interface IMPSCnnInstanceNormalizationDataSource { }

	[MacCatalyst (13, 1)]
#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject), Name = "MPSCNNInstanceNormalizationDataSource")]
	interface MPSCnnInstanceNormalizationDataSource : NSCopying {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Abstract]
		[Export ("gamma")]
		IntPtr /* float* */ Gamma { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Abstract]
		[Export ("beta")]
		IntPtr /* float* */ Beta { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Abstract]
		[Export ("numberOfFeatureChannels")]
		nuint NumberOfFeatureChannels { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Abstract]
		[Export ("label")]
		string Label { get; }

		[Export ("updateGammaAndBetaWithCommandBuffer:instanceNormalizationStateBatch:")]
		[return: NullAllowed]
		MPSCnnNormalizationGammaAndBetaState UpdateGammaAndBeta (IMTLCommandBuffer commandBuffer, MPSCnnInstanceNormalizationGradientState [] instanceNormalizationStateBatch);

		[Export ("updateGammaAndBetaWithInstanceNormalizationStateBatch:")]
		bool UpdateGammaAndBeta (MPSCnnInstanceNormalizationGradientState [] instanceNormalizationStateBatch);

		[Export ("epsilon")]
		float GetEpsilon ();

		[Export ("encodeWithCoder:")]
		void Encode (NSCoder coder);

		[Export ("initWithCoder:")]
		NativeHandle Constructor (NSCoder decoder);

		// This needs to be inlined in classes that implement 'IMPSCnnInstanceNormalizationDataSource'.
		//[Static]
		//[Export ("supportsSecureCoding")]
		//bool SupportsSecureCoding { get; }

		[MacCatalyst (13, 1)]
		[Export ("copyWithZone:device:")]
		[return: Release]
		IMPSCnnInstanceNormalizationDataSource Copy ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel), Name = "MPSCNNInstanceNormalization")]
	[DisableDefaultCtor]
	interface MPSCnnInstanceNormalization {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("epsilon")]
		float Epsilon { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("dataSource", ArgumentSemantic.Retain)]
		IMPSCnnInstanceNormalizationDataSource DataSource { get; }

		[Export ("initWithDevice:dataSource:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, IMPSCnnInstanceNormalizationDataSource dataSource);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);

		[Deprecated (PlatformName.TvOS, 12, 0, message: "Please use 'ReloadGammaAndBetaFromDataSource' instead.")]
		[Deprecated (PlatformName.iOS, 12, 0, message: "Please use 'ReloadGammaAndBetaFromDataSource' instead.")]
		[Deprecated (PlatformName.MacOSX, 10, 14, message: "Please use 'ReloadGammaAndBetaFromDataSource' instead.")]
		[Deprecated (PlatformName.MacCatalyst, 13, 1, message: "Please use 'ReloadGammaAndBetaFromDataSource' instead.")]
		[Export ("reloadDataSource:")]
		void ReloadDataSource (IMPSCnnInstanceNormalizationDataSource dataSource);

		[MacCatalyst (13, 1)]
		[Export ("reloadGammaAndBetaFromDataSource")]
		void ReloadGammaAndBetaFromDataSource ();

		[Export ("reloadGammaAndBetaWithCommandBuffer:gammaAndBetaState:")]
		void ReloadGammaAndBeta (IMTLCommandBuffer commandBuffer, MPSCnnNormalizationGammaAndBetaState gammaAndBetaState);

		[Export ("resultStateForSourceImage:sourceStates:destinationImage:")]
		[return: NullAllowed]
		MPSCnnInstanceNormalizationGradientState GetResultState (MPSImage sourceImage, [NullAllowed] NSArray<MPSState> sourceStates, MPSImage destinationImage);

		[Export ("temporaryResultStateForCommandBuffer:sourceImage:sourceStates:destinationImage:")]
		[return: NullAllowed]
		MPSCnnInstanceNormalizationGradientState GetTemporaryResultState (IMTLCommandBuffer commandBuffer, MPSImage sourceImage, [NullAllowed] NSArray<MPSState> sourceStates, MPSImage destinationImage);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnGradientKernel), Name = "MPSCNNInstanceNormalizationGradient")]
	[DisableDefaultCtor]
	interface MPSCnnInstanceNormalizationGradient {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnBinaryKernel), Name = "MPSCNNGradientKernel")]
	[DisableDefaultCtor]
	interface MPSCnnGradientKernel {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("kernelOffsetX")]
		nint KernelOffsetX { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("kernelOffsetY")]
		nint KernelOffsetY { get; set; }

		[Export ("encodeToCommandBuffer:sourceGradient:sourceImage:gradientState:")]
		MPSImage Encode (IMTLCommandBuffer commandBuffer, MPSImage sourceGradient, MPSImage sourceImage, MPSState gradientState);

		[Export ("encodeToCommandBuffer:sourceGradient:sourceImage:gradientState:destinationGradient:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSImage sourceGradient, MPSImage sourceImage, MPSState gradientState, MPSImage destinationGradient);

		[Export ("encodeBatchToCommandBuffer:sourceGradients:sourceImages:gradientStates:")]
		NSArray<MPSImage> EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceGradients, NSArray<MPSImage> sourceImages, NSArray<MPSState> gradientStates);

		[Export ("encodeBatchToCommandBuffer:sourceGradients:sourceImages:gradientStates:destinationGradients:")]
		void EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceGradients, NSArray<MPSImage> sourceImages, NSArray<MPSState> gradientStates, NSArray<MPSImage> destinationGradients);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject), Name = "MPSCNNLossDataDescriptor")]
	[DisableDefaultCtor]
	interface MPSCnnLossDataDescriptor : NSCopying {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("layout")]
		MPSDataLayout Layout { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("size")]
		MTLSize Size { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("bytesPerRow")]
		nuint BytesPerRow { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("bytesPerImage")]
		nuint BytesPerImage { get; set; }

		[Static]
		[Export ("cnnLossDataDescriptorWithData:layout:size:")]
		[return: NullAllowed]
		MPSCnnLossDataDescriptor Create (NSData data, MPSDataLayout layout, MTLSize size);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSState), Name = "MPSCNNLossLabels")]
	[DisableDefaultCtor]
	interface MPSCnnLossLabels {

		[Export ("initWithDevice:labelsDescriptor:")]
		NativeHandle Constructor (IMTLDevice device, MPSCnnLossDataDescriptor labelsDescriptor);

		[Export ("initWithDevice:lossImageSize:labelsDescriptor:weightsDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MTLSize lossImageSize, MPSCnnLossDataDescriptor labelsDescriptor, [NullAllowed] MPSCnnLossDataDescriptor weightsDescriptor);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("lossImage")]
		MPSImage LossImage { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("labelsImage")]
		MPSImage LabelsImage { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[MacCatalyst (13, 1)]
		[Export ("weightsImage")]
		MPSImage WeightsImage { get; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject), Name = "MPSCNNLossDescriptor")]
	[DisableDefaultCtor]
	interface MPSCnnLossDescriptor : NSCopying {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("lossType", ArgumentSemantic.Assign)]
		MPSCnnLossType LossType { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("reductionType", ArgumentSemantic.Assign)]
		MPSCnnReductionType ReductionType { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("weight")]
		float Weight { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("labelSmoothing")]
		float LabelSmoothing { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("numberOfClasses")]
		nuint NumberOfClasses { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("epsilon")]
		float Epsilon { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("delta")]
		float Delta { get; set; }

		[Static]
		[Export ("cnnLossDescriptorWithType:reductionType:")]
		MPSCnnLossDescriptor Create (MPSCnnLossType lossType, MPSCnnReductionType reductionType);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel), Name = "MPSCNNLoss")]
	[DisableDefaultCtor]
	interface MPSCnnLoss {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("lossType")]
		MPSCnnLossType LossType { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("reductionType")]
		MPSCnnReductionType ReductionType { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("weight")]
		float Weight { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("labelSmoothing")]
		float LabelSmoothing { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("numberOfClasses")]
		nuint NumberOfClasses { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("epsilon")]
		float Epsilon { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("delta")]
		float Delta { get; }

		[Export ("initWithDevice:lossDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSCnnLossDescriptor lossDescriptor);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);

		[Export ("encodeToCommandBuffer:sourceImage:labels:destinationImage:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSImage sourceImage, MPSCnnLossLabels labels, MPSImage destinationImage);

		[Export ("encodeToCommandBuffer:sourceImage:labels:")]
		MPSImage Encode (IMTLCommandBuffer commandBuffer, MPSImage sourceImage, MPSCnnLossLabels labels);

		[Export ("encodeBatchToCommandBuffer:sourceImages:labels:destinationImages:")]
		void EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceImage, NSArray<MPSCnnLossLabels> labels, NSArray<MPSImage> destinationImage);

		[Export ("encodeBatchToCommandBuffer:sourceImages:labels:")]
		NSArray<MPSImage> EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceImage, NSArray<MPSCnnLossLabels> labels);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject), Name = "MPSCNNYOLOLossDescriptor")]
	[DisableDefaultCtor]
	interface MPSCnnYoloLossDescriptor : NSCopying {

		[Export ("XYLossDescriptor", ArgumentSemantic.Retain)]
		MPSCnnLossDescriptor XYLossDescriptor { get; set; }

		[Export ("WHLossDescriptor", ArgumentSemantic.Retain)]
		MPSCnnLossDescriptor WHLossDescriptor { get; set; }

		[Export ("confidenceLossDescriptor", ArgumentSemantic.Retain)]
		MPSCnnLossDescriptor ConfidenceLossDescriptor { get; set; }

		[Export ("classesLossDescriptor", ArgumentSemantic.Retain)]
		MPSCnnLossDescriptor ClassesLossDescriptor { get; set; }

		[Export ("reductionType", ArgumentSemantic.Assign)]
		MPSCnnReductionType ReductionType { get; set; }

		[Export ("rescore")]
		bool Rescore { get; set; }

		[Export ("scaleXY")]
		float ScaleXY { get; set; }

		[Export ("scaleWH")]
		float ScaleWH { get; set; }

		[Export ("scaleNoObject")]
		float ScaleNoObject { get; set; }

		[Export ("scaleObject")]
		float ScaleObject { get; set; }

		[Export ("scaleClass")]
		float ScaleClass { get; set; }

		[Export ("minIOUForObjectPresence")]
		float MinIouForObjectPresence { get; set; }

		[Export ("maxIOUForObjectAbsence")]
		float MaxIouForObjectAbsence { get; set; }

		[Export ("numberOfAnchorBoxes")]
		nuint NumberOfAnchorBoxes { get; set; }

		[Export ("anchorBoxes", ArgumentSemantic.Retain)]
		NSData AnchorBoxes { get; set; }

		[Static]
		[Export ("cnnLossDescriptorWithXYLossType:WHLossType:confidenceLossType:classesLossType:reductionType:anchorBoxes:numberOfAnchorBoxes:")]
		MPSCnnYoloLossDescriptor Create (MPSCnnLossType xyLossType, MPSCnnLossType whLossType, MPSCnnLossType confidenceLossType, MPSCnnLossType classesLossType, MPSCnnReductionType reductionType, NSData anchorBoxes, nuint numberOfAnchorBoxes);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel), Name = "MPSCNNYOLOLoss")]
	[DisableDefaultCtor]
	interface MPSCnnYoloLoss {

		[Export ("lossXY", ArgumentSemantic.Retain)]
		MPSCnnLoss LossXY { get; }

		[Export ("lossWH", ArgumentSemantic.Retain)]
		MPSCnnLoss LossWH { get; }

		[Export ("lossConfidence", ArgumentSemantic.Retain)]
		MPSCnnLoss LossConfidence { get; }

		[Export ("lossClasses", ArgumentSemantic.Retain)]
		MPSCnnLoss LossClasses { get; }

		[Export ("scaleXY")]
		float ScaleXY { get; }

		[Export ("scaleWH")]
		float ScaleWH { get; }

		[Export ("scaleNoObject")]
		float ScaleNoObject { get; }

		[Export ("scaleObject")]
		float ScaleObject { get; }

		[Export ("scaleClass")]
		float ScaleClass { get; }

		[Export ("minIOUForObjectPresence")]
		float MinIouForObjectPresence { get; }

		[Export ("maxIOUForObjectAbsence")]
		float MaxIouForObjectAbsence { get; }

		[Export ("reductionType")]
		MPSCnnReductionType ReductionType { get; }

		[Export ("numberOfAnchorBoxes")]
		nuint NumberOfAnchorBoxes { get; }

		[Export ("anchorBoxes", ArgumentSemantic.Retain)]
		NSData AnchorBoxes { get; }

		[Export ("initWithDevice:lossDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSCnnYoloLossDescriptor lossDescriptor);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);

		[Export ("encodeToCommandBuffer:sourceImage:labels:destinationImage:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSImage sourceImage, MPSCnnLossLabels labels, MPSImage destinationImage);

		[Export ("encodeToCommandBuffer:sourceImage:labels:")]
		MPSImage Encode (IMTLCommandBuffer commandBuffer, MPSImage sourceImage, MPSCnnLossLabels labels);

		[Export ("encodeBatchToCommandBuffer:sourceImages:labels:destinationImages:")]
		void EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceImage, NSArray<MPSCnnLossLabels> labels, NSArray<MPSImage> destinationImage);

		[Export ("encodeBatchToCommandBuffer:sourceImages:labels:")]
		NSArray<MPSImage> EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> sourceImage, NSArray<MPSCnnLossLabels> labels);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNBinaryGradientState), Name = "MPSCNNArithmeticGradientState")]
	[DisableDefaultCtor]
	interface MPSCnnArithmeticGradientState {

	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnBinaryKernel), Name = "MPSCNNArithmetic")]
	[DisableDefaultCtor]
	interface MPSCnnArithmetic {

		// Inlined but you are not supposed to use this class, only its subclasses
		//[Export ("initWithDevice:")]
		//[DesignatedInitializer]
		//NativeHandle Constructor (IMTLDevice device);

		//[Export ("initWithCoder:device:")]
		//[DesignatedInitializer]
		//NativeHandle Constructor (NSCoder decoder, IMTLDevice device);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("primaryScale")]
		float PrimaryScale { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("secondaryScale")]
		float SecondaryScale { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("bias")]
		float Bias { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("primaryStrideInFeatureChannels")]
		nuint PrimaryStrideInFeatureChannels { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("secondaryStrideInFeatureChannels")]
		nuint SecondaryStrideInFeatureChannels { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("minimumValue")]
		float MinimumValue { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("maximumValue")]
		float MaximumValue { get; set; }

		[Export ("encodeToCommandBuffer:primaryImage:secondaryImage:destinationState:destinationImage:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSImage primaryImage, MPSImage secondaryImage, MPSCnnArithmeticGradientState destinationState, MPSImage destinationImage);

		[Export ("encodeBatchToCommandBuffer:primaryImages:secondaryImages:destinationStates:destinationImages:")]
		void EncodeBatch (IMTLCommandBuffer commandBuffer, NSArray<MPSImage> primaryImages, NSArray<MPSImage> secondaryImages, MPSCnnArithmeticGradientState [] destinationStates, NSArray<MPSImage> destinationImages);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnArithmetic), Name = "MPSCNNAdd")]
	[DisableDefaultCtor]
	interface MPSCnnAdd {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnArithmetic), Name = "MPSCNNSubtract")]
	[DisableDefaultCtor]
	interface MPSCnnSubtract {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnArithmetic), Name = "MPSCNNMultiply")]
	[DisableDefaultCtor]
	interface MPSCnnMultiply {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnArithmetic), Name = "MPSCNNDivide")]
	[DisableDefaultCtor]
	interface MPSCnnDivide {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnArithmetic))]
	[DisableDefaultCtor]
	interface MPSNNCompare {

		[Export ("comparisonType", ArgumentSemantic.Assign)]
		MPSNNComparisonType ComparisonType { get; set; }

		[Export ("threshold")]
		float Threshold { get; set; }

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnGradientKernel), Name = "MPSCNNArithmeticGradient")]
	[DisableDefaultCtor]
	interface MPSCnnArithmeticGradient {

		// Inlined but you are not supposed to use this class, only its subclasses
		//[Export ("initWithDevice:")]
		//[DesignatedInitializer]
		//NativeHandle Constructor (IMTLDevice device);

		//[Export ("initWithCoder:device:")]
		//[DesignatedInitializer]
		//NativeHandle Constructor (NSCoder decoder, IMTLDevice device);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("primaryScale")]
		float PrimaryScale { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("secondaryScale")]
		float SecondaryScale { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("bias")]
		float Bias { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("secondaryStrideInFeatureChannels")]
		nuint SecondaryStrideInFeatureChannels { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("minimumValue")]
		float MinimumValue { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("maximumValue")]
		float MaximumValue { get; set; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("isSecondarySourceFilter")]
		bool IsSecondarySourceFilter { get; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnArithmeticGradient), Name = "MPSCNNAddGradient")]
	[DisableDefaultCtor]
	interface MPSCnnAddGradient {

		[Export ("initWithDevice:isSecondarySourceFilter:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, bool isSecondarySourceFilter);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnArithmeticGradient), Name = "MPSCNNSubtractGradient")]
	[DisableDefaultCtor]
	interface MPSCnnSubtractGradient {

		[Export ("initWithDevice:isSecondarySourceFilter:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, bool isSecondarySourceFilter);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnArithmeticGradient), Name = "MPSCNNMultiplyGradient")]
	[DisableDefaultCtor]
	interface MPSCnnMultiplyGradient {

		[Export ("initWithDevice:isSecondarySourceFilter:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, bool isSecondarySourceFilter);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MPSNNNeuronDescriptor : NSCopying, NSSecureCoding {

		[Export ("neuronType", ArgumentSemantic.Assign)]
		MPSCnnNeuronType NeuronType { get; set; }

		[Export ("a")]
		float A { get; set; }

		[Export ("b")]
		float B { get; set; }

		[Export ("c")]
		float C { get; set; }

		[NullAllowed, Export ("data", ArgumentSemantic.Retain)]
		NSData Data { get; set; }

		[Static]
		[Export ("cnnNeuronDescriptorWithType:")]
		MPSNNNeuronDescriptor Create (MPSCnnNeuronType neuronType);

		[Static]
		[Export ("cnnNeuronDescriptorWithType:a:")]
		MPSNNNeuronDescriptor Create (MPSCnnNeuronType neuronType, float a);

		[Static]
		[Export ("cnnNeuronDescriptorWithType:a:b:")]
		MPSNNNeuronDescriptor Create (MPSCnnNeuronType neuronType, float a, float b);

		[Static]
		[Export ("cnnNeuronDescriptorWithType:a:b:c:")]
		MPSNNNeuronDescriptor Create (MPSCnnNeuronType neuronType, float a, float b, float c);

		[Static]
		[Export ("cnnNeuronPReLUDescriptorWithData:noCopy:")]
		MPSNNNeuronDescriptor Create (NSData data, bool noCopy);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnGradientKernel), Name = "MPSCNNNeuronGradient")]
	[DisableDefaultCtor]
	interface MPSCnnNeuronGradient {

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("neuronType")]
		MPSCnnNeuronType NeuronType { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("a")]
		float A { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("b")]
		float B { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[Export ("c")]
		float C { get; }

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[NullAllowed, Export ("data", ArgumentSemantic.Retain)]
		NSData Data { get; }

		[Export ("initWithDevice:neuronDescriptor:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSNNNeuronDescriptor neuronDescriptor);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSState), Name = "MPSCNNNormalizationGammaAndBetaState")]
	[DisableDefaultCtor]
	interface MPSCnnNormalizationGammaAndBetaState {

		[Export ("gamma")]
		IMTLBuffer Gamma { get; }

		[Export ("beta")]
		IMTLBuffer Beta { get; }

		[Export ("initWithGamma:beta:")]
		NativeHandle Constructor (IMTLBuffer gamma, IMTLBuffer beta);

		[Static]
		[Export ("temporaryStateWithCommandBuffer:numberOfFeatureChannels:")]
		MPSCnnNormalizationGammaAndBetaState GetTemporaryState (IMTLCommandBuffer commandBuffer, nuint numberOfFeatureChannels);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixUnaryKernel))]
	[DisableDefaultCtor]
	interface MPSMatrixBatchNormalization {

		[Export ("sourceNumberOfFeatureVectors")]
		nuint SourceNumberOfFeatureVectors { get; set; }

		[Export ("sourceInputFeatureChannels")]
		nuint SourceInputFeatureChannels { get; set; }

		[Export ("epsilon")]
		float Epsilon { get; set; }

		[Export ("computeStatistics")]
		bool ComputeStatistics { get; set; }

		[Export ("setNeuronType:parameterA:parameterB:parameterC:")]
		void SetNeuronType (MPSCnnNeuronType neuronType, float parameterA, float parameterB, float parameterC);

		[Export ("neuronType")]
		MPSCnnNeuronType NeuronType { get; }

		[Export ("neuronParameterA")]
		float NeuronParameterA { get; }

		[Export ("neuronParameterB")]
		float NeuronParameterB { get; }

		[Export ("neuronParameterC")]
		float NeuronParameterC { get; }

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("encodeToCommandBuffer:inputMatrix:meanVector:varianceVector:gammaVector:betaVector:resultMatrix:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSMatrix inputMatrix, MPSVector meanVector, MPSVector varianceVector, [NullAllowed] MPSVector gammaVector, [NullAllowed] MPSVector betaVector, MPSMatrix resultMatrix);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);

		[Export ("copyWithZone:device:")]
		[return: Release]
		MPSMatrixBatchNormalization Copy ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSMatrixBinaryKernel))]
	[DisableDefaultCtor]
	interface MPSMatrixBatchNormalizationGradient {

		[Export ("sourceNumberOfFeatureVectors")]
		nuint SourceNumberOfFeatureVectors { get; set; }

		[Export ("sourceInputFeatureChannels")]
		nuint SourceInputFeatureChannels { get; set; }

		[Export ("epsilon")]
		float Epsilon { get; set; }

		[Export ("setNeuronType:parameterA:parameterB:parameterC:")]
		void SetNeuronType (MPSCnnNeuronType neuronType, float parameterA, float parameterB, float parameterC);

		[Export ("neuronType")]
		MPSCnnNeuronType NeuronType { get; }

		[Export ("neuronParameterA")]
		float NeuronParameterA { get; }

		[Export ("neuronParameterB")]
		float NeuronParameterB { get; }

		[Export ("neuronParameterC")]
		float NeuronParameterC { get; }

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("encodeToCommandBuffer:gradientMatrix:inputMatrix:meanVector:varianceVector:gammaVector:betaVector:resultGradientForDataMatrix:resultGradientForGammaVector:resultGradientForBetaVector:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSMatrix gradientMatrix, MPSMatrix inputMatrix, MPSVector meanVector, MPSVector varianceVector, [NullAllowed] MPSVector gammaVector, [NullAllowed] MPSVector betaVector, MPSMatrix resultGradientForDataMatrix, [NullAllowed] MPSVector resultGradientForGammaVector, [NullAllowed] MPSVector resultGradientForBetaVector);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);

		[Export ("copyWithZone:device:")]
		[return: Release]
		MPSMatrixBatchNormalizationGradient Copy ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSState))]
	[DisableDefaultCtor]
	interface MPSNNGradientState {

		[New]
		[Static]
		[Export ("temporaryStateWithCommandBuffer:bufferSize:")]
		MPSNNGradientState CreateTemporaryState (IMTLCommandBuffer commandBuffer, nuint bufferSize);

		[New]
		[Static]
		[Export ("temporaryStateWithCommandBuffer:textureDescriptor:")]
		MPSNNGradientState CreateTemporaryState (IMTLCommandBuffer commandBuffer, MTLTextureDescriptor descriptor);

		[New]
		[Static]
		[Export ("temporaryStateWithCommandBuffer:")]
		MPSNNGradientState CreateTemporaryState (IMTLCommandBuffer commandBuffer);

		[Export ("initWithDevice:bufferSize:")]
		NativeHandle Constructor (IMTLDevice device, nuint bufferSize);

		[Export ("initWithDevice:textureDescriptor:")]
		NativeHandle Constructor (IMTLDevice device, MTLTextureDescriptor descriptor);

		[Export ("initWithResource:")]
		NativeHandle Constructor ([NullAllowed] IMTLResource resource);

		[Export ("initWithDevice:resourceList:")]
		NativeHandle Constructor (IMTLDevice device, MPSStateResourceList resourceList);

		[New]
		[Static]
		[Export ("temporaryStateWithCommandBuffer:resourceList:")]
		MPSNNGradientState CreateTemporaryState (IMTLCommandBuffer commandBuffer, MPSStateResourceList resourceList);

		[Export ("initWithResources:")]
		NativeHandle Constructor ([NullAllowed] IMTLResource [] resources);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSState))]
	[DisableDefaultCtor]
	interface MPSNNBinaryGradientState {

		[New]
		[Static]
		[Export ("temporaryStateWithCommandBuffer:bufferSize:")]
		MPSNNBinaryGradientState CreateTemporaryState (IMTLCommandBuffer commandBuffer, nuint bufferSize);

		[New]
		[Static]
		[Export ("temporaryStateWithCommandBuffer:textureDescriptor:")]
		MPSNNBinaryGradientState CreateTemporaryState (IMTLCommandBuffer commandBuffer, MTLTextureDescriptor descriptor);

		[New]
		[Static]
		[Export ("temporaryStateWithCommandBuffer:")]
		MPSNNBinaryGradientState CreateTemporaryState (IMTLCommandBuffer commandBuffer);

		[Export ("initWithDevice:bufferSize:")]
		NativeHandle Constructor (IMTLDevice device, nuint bufferSize);

		[Export ("initWithDevice:textureDescriptor:")]
		NativeHandle Constructor (IMTLDevice device, MTLTextureDescriptor descriptor);

		[Export ("initWithResource:")]
		NativeHandle Constructor ([NullAllowed] IMTLResource resource);

		[Export ("initWithDevice:resourceList:")]
		NativeHandle Constructor (IMTLDevice device, MPSStateResourceList resourceList);

		[New]
		[Static]
		[Export ("temporaryStateWithCommandBuffer:resourceList:")]
		MPSNNBinaryGradientState CreateTemporaryState (IMTLCommandBuffer commandBuffer, MPSStateResourceList resourceList);

		[Export ("initWithResources:")]
		NativeHandle Constructor ([NullAllowed] IMTLResource [] resources);
	}

	interface IMPSNNTrainableNode { }

	[MacCatalyst (13, 1)]
	[Protocol]
	interface MPSNNTrainableNode {
		[Abstract]
		[Export ("trainingStyle", ArgumentSemantic.Assign)]
		MPSNNTrainingStyle TrainingStyle { get; set; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MPSNNOptimizerDescriptor {

		[Export ("learningRate")]
		float LearningRate { get; set; }

		[Export ("gradientRescale")]
		float GradientRescale { get; set; }

		[Export ("applyGradientClipping")]
		bool ApplyGradientClipping { get; set; }

		[Export ("gradientClipMax")]
		float GradientClipMax { get; set; }

		[Export ("gradientClipMin")]
		float GradientClipMin { get; set; }

		[Export ("regularizationScale")]
		float RegularizationScale { get; set; }

		[Export ("regularizationType", ArgumentSemantic.Assign)]
		MPSNNRegularizationType RegularizationType { get; set; }

		[Export ("initWithLearningRate:gradientRescale:regularizationType:regularizationScale:")]
		NativeHandle Constructor (float learningRate, float gradientRescale, MPSNNRegularizationType regularizationType, float regularizationScale);

		[Export ("initWithLearningRate:gradientRescale:applyGradientClipping:gradientClipMax:gradientClipMin:regularizationType:regularizationScale:")]
		NativeHandle Constructor (float learningRate, float gradientRescale, bool applyGradientClipping, float gradientClipMax, float gradientClipMin, MPSNNRegularizationType regularizationType, float regularizationScale);

		[Static]
		[Export ("optimizerDescriptorWithLearningRate:gradientRescale:regularizationType:regularizationScale:")]
		MPSNNOptimizerDescriptor Create (float learningRate, float gradientRescale, MPSNNRegularizationType regularizationType, float regularizationScale);

		[Static]
		[Export ("optimizerDescriptorWithLearningRate:gradientRescale:applyGradientClipping:gradientClipMax:gradientClipMin:regularizationType:regularizationScale:")]
		MPSNNOptimizerDescriptor Create (float learningRate, float gradientRescale, bool applyGradientClipping, float gradientClipMax, float gradientClipMin, MPSNNRegularizationType regularizationType, float regularizationScale);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel))]
	[DisableDefaultCtor] // You must use one of the sub-classes of MPSNNOptimizer.
	interface MPSNNOptimizer {

		[Export ("learningRate")]
		float LearningRate { get; }

		[Export ("gradientRescale")]
		float GradientRescale { get; }

		[Export ("applyGradientClipping")]
		bool ApplyGradientClipping { get; set; }

		[Export ("gradientClipMax")]
		float GradientClipMax { get; }

		[Export ("gradientClipMin")]
		float GradientClipMin { get; }

		[Export ("regularizationScale")]
		float RegularizationScale { get; }

		[Export ("regularizationType")]
		MPSNNRegularizationType RegularizationType { get; }

		[Export ("setLearningRate:")]
		void SetLearningRate (float newLearningRate);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNOptimizer))]
	[DisableDefaultCtor]
	interface MPSNNOptimizerStochasticGradientDescent {

		[Export ("momentumScale")]
		float MomentumScale { get; }

		[Export ("useNestrovMomentum")]
		bool UseNestrovMomentum { get; }

		[Export ("initWithDevice:learningRate:")]
		NativeHandle Constructor (IMTLDevice device, float learningRate);

		[Export ("initWithDevice:momentumScale:useNestrovMomentum:optimizerDescriptor:")]
		NativeHandle Constructor (IMTLDevice device, float momentumScale, bool useNestrovMomentum, MPSNNOptimizerDescriptor optimizerDescriptor);

		[Export ("encodeToCommandBuffer:inputGradientVector:inputValuesVector:inputMomentumVector:resultValuesVector:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSVector inputGradientVector, MPSVector inputValuesVector, [NullAllowed] MPSVector inputMomentumVector, MPSVector resultValuesVector);

		[Export ("encodeToCommandBuffer:convolutionGradientState:convolutionSourceState:inputMomentumVectors:resultState:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSCnnConvolutionGradientState convolutionGradientState, MPSCnnConvolutionWeightsAndBiasesState convolutionSourceState, [NullAllowed] NSArray<MPSVector> inputMomentumVectors, MPSCnnConvolutionWeightsAndBiasesState resultState);

		[Export ("encodeToCommandBuffer:batchNormalizationState:inputMomentumVectors:resultState:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSCnnBatchNormalizationState batchNormalizationState, [NullAllowed] NSArray<MPSVector> inputMomentumVectors, MPSCnnNormalizationGammaAndBetaState resultState);

		[Export ("encodeToCommandBuffer:batchNormalizationGradientState:batchNormalizationSourceState:inputMomentumVectors:resultState:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSCnnBatchNormalizationState batchNormalizationGradientState, MPSCnnBatchNormalizationState batchNormalizationSourceState, [NullAllowed] NSArray<MPSVector> inputMomentumVectors, MPSCnnNormalizationGammaAndBetaState resultState);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNOptimizer), Name = "MPSNNOptimizerRMSProp")]
	[DisableDefaultCtor]
	interface MPSNNOptimizerRmsProp {

		[Export ("decay")]
		double Decay { get; }

		[Export ("epsilon")]
		float Epsilon { get; }

		[Export ("initWithDevice:learningRate:")]
		NativeHandle Constructor (IMTLDevice device, float learningRate);

		[Export ("initWithDevice:decay:epsilon:optimizerDescriptor:")]
		NativeHandle Constructor (IMTLDevice device, double decay, float epsilon, MPSNNOptimizerDescriptor optimizerDescriptor);

		[Export ("encodeToCommandBuffer:inputGradientVector:inputValuesVector:inputSumOfSquaresVector:resultValuesVector:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSVector inputGradientVector, MPSVector inputValuesVector, MPSVector inputSumOfSquaresVector, MPSVector resultValuesVector);

		[Export ("encodeToCommandBuffer:convolutionGradientState:convolutionSourceState:inputSumOfSquaresVectors:resultState:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSCnnConvolutionGradientState convolutionGradientState, MPSCnnConvolutionWeightsAndBiasesState convolutionSourceState, [NullAllowed] NSArray<MPSVector> inputSumOfSquaresVectors, MPSCnnConvolutionWeightsAndBiasesState resultState);

		[Export ("encodeToCommandBuffer:batchNormalizationState:inputSumOfSquaresVectors:resultState:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSCnnBatchNormalizationState batchNormalizationState, [NullAllowed] NSArray<MPSVector> inputSumOfSquaresVectors, MPSCnnNormalizationGammaAndBetaState resultState);

		[Export ("encodeToCommandBuffer:batchNormalizationGradientState:batchNormalizationSourceState:inputSumOfSquaresVectors:resultState:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSCnnBatchNormalizationState batchNormalizationGradientState, MPSCnnBatchNormalizationState batchNormalizationSourceState, [NullAllowed] NSArray<MPSVector> inputSumOfSquaresVectors, MPSCnnNormalizationGammaAndBetaState resultState);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNOptimizer))]
	[DisableDefaultCtor]
	interface MPSNNOptimizerAdam {

		[Export ("beta1")]
		double Beta1 { get; }

		[Export ("beta2")]
		double Beta2 { get; }

		[Export ("epsilon")]
		float Epsilon { get; }

		[Export ("timeStep")]
		nuint TimeStep { get; set; }

		[Export ("initWithDevice:learningRate:")]
		NativeHandle Constructor (IMTLDevice device, float learningRate);

		[Export ("initWithDevice:beta1:beta2:epsilon:timeStep:optimizerDescriptor:")]
		NativeHandle Constructor (IMTLDevice device, double beta1, double beta2, float epsilon, nuint timeStep, MPSNNOptimizerDescriptor optimizerDescriptor);

		[Export ("encodeToCommandBuffer:inputGradientVector:inputValuesVector:inputMomentumVector:inputVelocityVector:resultValuesVector:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSVector inputGradientVector, MPSVector inputValuesVector, MPSVector inputMomentumVector, MPSVector inputVelocityVector, MPSVector resultValuesVector);

		[Export ("encodeToCommandBuffer:convolutionGradientState:convolutionSourceState:inputMomentumVectors:inputVelocityVectors:resultState:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSCnnConvolutionGradientState convolutionGradientState, MPSCnnConvolutionWeightsAndBiasesState convolutionSourceState, [NullAllowed] NSArray<MPSVector> inputMomentumVectors, [NullAllowed] NSArray<MPSVector> inputVelocityVectors, MPSCnnConvolutionWeightsAndBiasesState resultState);

		[Export ("encodeToCommandBuffer:batchNormalizationState:inputMomentumVectors:inputVelocityVectors:resultState:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSCnnBatchNormalizationState batchNormalizationState, [NullAllowed] NSArray<MPSVector> inputMomentumVectors, [NullAllowed] NSArray<MPSVector> inputVelocityVectors, MPSCnnNormalizationGammaAndBetaState resultState);

		[Export ("encodeToCommandBuffer:batchNormalizationGradientState:batchNormalizationSourceState:inputMomentumVectors:inputVelocityVectors:resultState:")]
		void Encode (IMTLCommandBuffer commandBuffer, MPSCnnBatchNormalizationState batchNormalizationGradientState, MPSCnnBatchNormalizationState batchNormalizationSourceState, [NullAllowed] NSArray<MPSVector> inputMomentumVectors, [NullAllowed] NSArray<MPSVector> inputVelocityVectors, MPSCnnNormalizationGammaAndBetaState resultState);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel))]
	[DisableDefaultCtor] // You must use one of the sub-classes of MPSNNReduceUnary.
	interface MPSNNReduceUnary {

		[Export ("clipRectSource", ArgumentSemantic.Assign)]
		MTLRegion ClipRectSource { get; set; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNReduceUnary))]
	[DisableDefaultCtor]
	interface MPSNNReduceRowMin {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNReduceUnary))]
	[DisableDefaultCtor]
	interface MPSNNReduceColumnMin {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNReduceUnary))]
	[DisableDefaultCtor]
	interface MPSNNReduceFeatureChannelsMin {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNReduceUnary))]
	[DisableDefaultCtor]
	interface MPSNNReduceFeatureChannelsArgumentMin {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNReduceUnary))]
	[DisableDefaultCtor]
	interface MPSNNReduceRowMax {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNReduceUnary))]
	[DisableDefaultCtor]
	interface MPSNNReduceColumnMax {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNReduceUnary))]
	[DisableDefaultCtor]
	interface MPSNNReduceFeatureChannelsMax {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNReduceUnary))]
	[DisableDefaultCtor]
	interface MPSNNReduceFeatureChannelsArgumentMax {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNReduceUnary))]
	[DisableDefaultCtor]
	interface MPSNNReduceRowMean {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNReduceUnary))]
	[DisableDefaultCtor]
	interface MPSNNReduceColumnMean {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNReduceUnary))]
	[DisableDefaultCtor]
	interface MPSNNReduceFeatureChannelsMean {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNReduceUnary))]
	[DisableDefaultCtor]
	interface MPSNNReduceRowSum {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNReduceUnary))]
	[DisableDefaultCtor]
	interface MPSNNReduceColumnSum {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNReduceUnary))]
	[DisableDefaultCtor]
	interface MPSNNReduceFeatureChannelsSum {

		[Export ("weight")]
		float Weight { get; set; }

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnBinaryKernel))]
	[DisableDefaultCtor] // You must use one of the sub-classes of MPSNNReduceBinary.
	interface MPSNNReduceBinary {

		[Export ("primarySourceClipRect", ArgumentSemantic.Assign)]
		MTLRegion PrimarySourceClipRect { get; set; }

		[Export ("secondarySourceClipRect", ArgumentSemantic.Assign)]
		MTLRegion SecondarySourceClipRect { get; set; }
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNReduceBinary))]
	[DisableDefaultCtor]
	interface MPSNNReduceFeatureChannelsAndWeightsMean {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSNNReduceBinary))]
	[DisableDefaultCtor]
	interface MPSNNReduceFeatureChannelsAndWeightsSum {

		[Export ("doWeightedSumByNonZeroWeights")]
		bool DoWeightedSumByNonZeroWeights { get; }

		[Export ("initWithDevice:")]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithDevice:doWeightedSumByNonZeroWeights:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, bool doWeightedSumByNonZeroWeights);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel))]
	[DisableDefaultCtor]
	interface MPSNNReshape {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnGradientKernel))]
	[DisableDefaultCtor]
	interface MPSNNReshapeGradient {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel))]
	[DisableDefaultCtor]
	interface MPSNNPad {

		[Export ("paddingSizeBefore", ArgumentSemantic.Assign)]
		MPSImageCoordinate PaddingSizeBefore { get; set; }

		[Export ("paddingSizeAfter", ArgumentSemantic.Assign)]
		MPSImageCoordinate PaddingSizeAfter { get; set; }

		[Export ("fillValue")]
		float FillValue { get; set; }

		[Export ("initWithDevice:")]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithDevice:paddingSizeBefore:paddingSizeAfter:")]
		NativeHandle Constructor (IMTLDevice device, MPSImageCoordinate paddingSizeBefore, MPSImageCoordinate paddingSizeAfter);

		[Export ("initWithDevice:paddingSizeBefore:paddingSizeAfter:fillValueArray:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSImageCoordinate paddingSizeBefore, MPSImageCoordinate paddingSizeAfter, [NullAllowed] NSData fillValueArray);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnGradientKernel))]
	[DisableDefaultCtor]
	interface MPSNNPadGradient {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel))]
	[DisableDefaultCtor]
	interface MPSNNResizeBilinear {

		[Export ("resizeWidth")]
		nuint ResizeWidth { get; }

		[Export ("resizeHeight")]
		nuint ResizeHeight { get; }

		[Export ("alignCorners")]
		bool AlignCorners { get; }

		[Export ("initWithDevice:resizeWidth:resizeHeight:alignCorners:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint resizeWidth, nuint resizeHeight, bool alignCorners);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel))]
	[DisableDefaultCtor]
	interface MPSNNCropAndResizeBilinear {

		[Export ("resizeWidth")]
		nuint ResizeWidth { get; }

		[Export ("resizeHeight")]
		nuint ResizeHeight { get; }

		[Export ("numberOfRegions")]
		nuint NumberOfRegions { get; }

		[Export ("regions")]
		IntPtr Regions { get; }

		[Export ("initWithDevice:resizeWidth:resizeHeight:numberOfRegions:regions:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint resizeWidth, nuint resizeHeight, nuint numberOfRegions, IntPtr regions);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSCnnKernel))]
	[DisableDefaultCtor]
	interface MPSNNSlice {

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSState), Name = "MPSRNNMatrixTrainingState")]
	[DisableDefaultCtor]
	interface MPSRnnMatrixTrainingState {

		[Static]
		[New]
		[Export ("temporaryStateWithCommandBuffer:bufferSize:")]
		MPSRnnMatrixTrainingState CreateTemporaryState (IMTLCommandBuffer commandBuffer, nuint bufferSize);

		[Static]
		[New]
		[Export ("temporaryStateWithCommandBuffer:textureDescriptor:")]
		MPSRnnMatrixTrainingState CreateTemporaryState (IMTLCommandBuffer commandBuffer, MTLTextureDescriptor descriptor);

		[Static]
		[New]
		[Export ("temporaryStateWithCommandBuffer:")]
		MPSRnnMatrixTrainingState CreateTemporaryState (IMTLCommandBuffer commandBuffer);

		[Export ("initWithDevice:bufferSize:")]
		NativeHandle Constructor (IMTLDevice device, nuint bufferSize);

		[Export ("initWithDevice:textureDescriptor:")]
		NativeHandle Constructor (IMTLDevice device, MTLTextureDescriptor descriptor);

		[Export ("initWithResource:")]
		NativeHandle Constructor ([NullAllowed] IMTLResource resource);

		[Export ("initWithDevice:resourceList:")]
		NativeHandle Constructor (IMTLDevice device, MPSStateResourceList resourceList);

		[Static]
		[New]
		[Export ("temporaryStateWithCommandBuffer:resourceList:")]
		MPSRnnMatrixTrainingState CreateTemporaryState (IMTLCommandBuffer commandBuffer, MPSStateResourceList resourceList);

		[Export ("initWithResources:")]
		NativeHandle Constructor ([NullAllowed] IMTLResource [] resources);
	}

	[MacCatalyst (13, 1)]
	[BaseType (typeof (MPSKernel), Name = "MPSRNNMatrixTrainingLayer")]
	[DisableDefaultCtor]
	interface MPSRnnMatrixTrainingLayer {

		[Export ("inputFeatureChannels")]
		nuint InputFeatureChannels { get; }

		[Export ("outputFeatureChannels")]
		nuint OutputFeatureChannels { get; }

		[Export ("storeAllIntermediateStates")]
		bool StoreAllIntermediateStates { get; set; }

		[Export ("recurrentOutputIsTemporary")]
		bool RecurrentOutputIsTemporary { get; set; }

		[Export ("trainingStateIsTemporary")]
		bool TrainingStateIsTemporary { get; set; }

		[Export ("accumulateWeightGradients")]
		bool AccumulateWeightGradients { get; set; }

		[Export ("initWithDevice:rnnDescriptor:trainableWeights:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, MPSRnnDescriptor rnnDescriptor, NSMutableArray<MPSMatrix> trainableWeights);

		[Export ("createWeightGradientMatrices:dataType:")]
		void CreateWeightGradientMatrices (NSMutableArray<MPSMatrix> matrices, MPSDataType dataType);

		[Export ("createTemporaryWeightGradientMatrices:dataType:commandBuffer:")]
		void CreateTemporaryWeightGradientMatrices (NSMutableArray<MPSMatrix> matrices, MPSDataType dataType, IMTLCommandBuffer commandBuffer);

		[Export ("createWeightMatrices:")]
		void CreateWeightMatrices (NSMutableArray<MPSMatrix> matrices);

		[Export ("encodeCopyWeightsToCommandBuffer:weights:matrixId:matrix:copyFromWeightsToMatrix:matrixOffset:")]
		void EncodeCopyWeights (IMTLCommandBuffer commandBuffer, MPSMatrix [] weights, MPSRnnMatrixId matrixId, MPSMatrix matrix, bool copyFromWeightsToMatrix, MTLOrigin matrixOffset);

		[Export ("encodeForwardSequenceToCommandBuffer:sourceMatrices:sourceOffsets:destinationMatrices:destinationOffsets:trainingStates:recurrentInputState:recurrentOutputStates:weights:")]
		void EncodeForwardSequence (IMTLCommandBuffer commandBuffer, MPSMatrix [] sourceMatrices, [NullAllowed] IntPtr sourceOffsets, MPSMatrix [] destinationMatrices, [NullAllowed] IntPtr destinationOffsets, NSMutableArray<MPSRnnMatrixTrainingState> trainingStates, [NullAllowed] MPSRnnRecurrentMatrixState recurrentInputState, [NullAllowed] NSMutableArray<MPSRnnRecurrentMatrixState> recurrentOutputStates, MPSMatrix [] weights);

		[Export ("encodeForwardSequenceToCommandBuffer:sourceMatrices:destinationMatrices:trainingStates:weights:")]
		void EncodeForwardSequence (IMTLCommandBuffer commandBuffer, MPSMatrix [] sourceMatrices, MPSMatrix [] destinationMatrices, NSMutableArray<MPSRnnMatrixTrainingState> trainingStates, MPSMatrix [] weights);

		[Export ("encodeGradientSequenceToCommandBuffer:forwardSources:forwardSourceOffsets:sourceGradients:sourceGradientOffsets:destinationGradients:destinationOffsets:weightGradients:trainingStates:recurrentInputState:recurrentOutputStates:weights:")]
		void EncodeGradientSequence (IMTLCommandBuffer commandBuffer, MPSMatrix [] forwardSources, [NullAllowed] IntPtr forwardSourceOffsets, MPSMatrix [] sourceGradients, [NullAllowed] IntPtr sourceGradientOffsets, [NullAllowed] MPSMatrix [] destinationGradients, [NullAllowed] IntPtr destinationOffsets, [NullAllowed] MPSMatrix [] weightGradients, MPSRnnMatrixTrainingState [] trainingStates, [NullAllowed] MPSRnnRecurrentMatrixState recurrentInputState, [NullAllowed] NSMutableArray<MPSRnnRecurrentMatrixState> recurrentOutputStates, MPSMatrix [] weights);

		[Export ("encodeGradientSequenceToCommandBuffer:forwardSources:sourceGradients:destinationGradients:weightGradients:trainingStates:weights:")]
		void EncodeGradientSequence (IMTLCommandBuffer commandBuffer, MPSMatrix [] forwardSources, MPSMatrix [] sourceGradients, [NullAllowed] MPSMatrix [] destinationGradients, [NullAllowed] MPSMatrix [] weightGradients, MPSRnnMatrixTrainingState [] trainingStates, MPSMatrix [] weights);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder decoder, IMTLDevice device);

		[Export ("copyWithZone:device:")]
		[return: Release]
		MPSRnnMatrixTrainingLayer Copy ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);
	}

	[TV (13, 0), Mac (10, 15), iOS (13, 0), MacCatalyst (13, 0)]
	[BaseType (typeof (MPSKernel))]
	[DisableDefaultCtor]
	interface MPSNDArrayMultiaryBase {
		[Export ("offsetsAtSourceIndex:")]
		[Deprecated (PlatformName.iOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacCatalyst, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.TvOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacOSX, 11, 0, message: "Use derived filter properties instead.")]
		MPSNDArrayOffsets GetOffsets (nuint sourceIndex);

		[Export ("edgeModeAtSourceIndex:")]
		[Deprecated (PlatformName.iOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacCatalyst, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.TvOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacOSX, 11, 0, message: "Use derived filter properties instead.")]
		MPSImageEdgeMode GetEdgeMode (nuint sourceIndex);

		[Export ("kernelSizesForSourceIndex:")]
		[Deprecated (PlatformName.iOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacCatalyst, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.TvOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacOSX, 11, 0, message: "Use derived filter properties instead.")]
		MPSNDArraySizes GetKernelSizes (nuint sourceIndex);

		[Export ("stridesForSourceIndex:")]
		[Deprecated (PlatformName.iOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacCatalyst, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.TvOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacOSX, 11, 0, message: "Use derived filter properties instead.")]
		MPSNDArrayOffsets GetStrides (nuint sourceIndex);

		[Export ("dilationRatesForSourceIndex:")]
		[Deprecated (PlatformName.iOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacCatalyst, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.TvOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacOSX, 11, 0, message: "Use derived filter properties instead.")]
		MPSNDArraySizes GetDilationRates (nuint sourceIndex);

		[Export ("destinationArrayAllocator", ArgumentSemantic.Retain)]
		IMPSNDArrayAllocator DestinationArrayAllocator { get; set; }

		[Export ("initWithDevice:sourceCount:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint count);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("encodeWithCoder:")]
		void EncodeTo (NSCoder coder);

		[Export ("copyWithZone:device:")]
		[return: Release ()]
		MPSMatrixNeuron Copy ([NullAllowed] NSZone zone, [NullAllowed] IMTLDevice device);

		[Export ("resultStateForSourceArrays:sourceStates:destinationArray:")]
		[return: NullAllowed]
		MPSState GetResultState (MPSNDArray [] sourceArrays, [NullAllowed] MPSState [] sourceStates, MPSNDArray destinationArray);

		[Export ("destinationArrayDescriptorForSourceArrays:sourceState:")]
		MPSNDArrayDescriptor GetDestinationArray (MPSNDArray [] sources, [NullAllowed] MPSState state);
	}

	[TV (13, 0), Mac (10, 15), iOS (13, 0), MacCatalyst (13, 0)]
	[BaseType (typeof (MPSNDArrayMultiaryBase))]
	[DisableDefaultCtor]
	interface MPSNDArrayMultiaryKernel {
		[Export ("initWithDevice:sourceCount:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device, nuint count);

		[Export ("initWithCoder:device:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSCoder aDecoder, IMTLDevice device);

		[Export ("encodeToCommandBuffer:sourceArrays:")]
		MPSNDArray EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSNDArray [] sourceArrays);

		[Export ("encodeToCommandBuffer:sourceArrays:destinationArray:")]
		MPSNDArray EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSNDArray [] sourceArrays, MPSNDArray destinationArray);

		[Export ("encodeToCommandBuffer:sourceArrays:resultState:outputStateIsTemporary:")]
		MPSNDArray EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSNDArray [] sourceArrays, [NullAllowed] out MPSState gradientState, bool outputStateIsTemporary);

		[Export ("encodeToCommandBuffer:sourceArrays:resultState:destinationArray:")]
		MPSNDArray EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSNDArray [] sourceArrays, [NullAllowed] MPSState gradientState, MPSNDArray destinationArray);

		[iOS (18, 0), TV (18, 0), MacCatalyst (18, 0), Mac (15, 0)]
		[Export ("encodeToCommandEncoder:commandBuffer:sourceArrays:destinationArray:")]
		MPSNDArray EncodeToCommandEncoder ([NullAllowed] IMTLComputeCommandEncoder encoder, IMTLCommandBuffer commandBuffer, MPSNDArray [] sourceArrays, MPSNDArray destinationArray);
	}

	[TV (13, 0), Mac (10, 15), iOS (13, 0), MacCatalyst (13, 0)]
	[BaseType (typeof (MPSNDArrayMultiaryKernel))]
	[DisableDefaultCtor]
	interface MPSNDArrayUnaryKernel {
		[Export ("offsets")]
		[Deprecated (PlatformName.iOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacCatalyst, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.TvOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacOSX, 11, 0, message: "Use derived filter properties instead.")]
		MPSNDArrayOffsets Offsets { get; }

		[Export ("edgeMode")]
		[Deprecated (PlatformName.iOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacCatalyst, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.TvOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacOSX, 11, 0, message: "Use derived filter properties instead.")]
		MPSImageEdgeMode EdgeMode { get; }

		[Export ("kernelSizes")]
		[Deprecated (PlatformName.iOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacCatalyst, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.TvOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacOSX, 11, 0, message: "Use derived filter properties instead.")]
		MPSNDArraySizes KernelSizes { get; }

		[Export ("strides")]
		[Deprecated (PlatformName.iOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacCatalyst, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.TvOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacOSX, 11, 0, message: "Use derived filter properties instead.")]
		MPSNDArrayOffsets Strides { get; }

		[Export ("dilationRates")]
		[Deprecated (PlatformName.iOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacCatalyst, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.TvOS, 14, 0, message: "Use derived filter properties instead.")]
		[Deprecated (PlatformName.MacOSX, 11, 0, message: "Use derived filter properties instead.")]
		MPSNDArraySizes DilationRates { get; }

		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("encodeToCommandBuffer:sourceArray:")]
		MPSNDArray EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSNDArray sourceArray);

		[Export ("encodeToCommandBuffer:sourceArray:destinationArray:")]
		MPSNDArray EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSNDArray sourceArray, MPSNDArray destinationArray);

		[Export ("encodeToCommandBuffer:sourceArray:resultState:outputStateIsTemporary:")]
		MPSNDArray EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSNDArray sourceArray, [NullAllowed] out MPSState gradientState, bool outputStateIsTemporary);

		[Export ("encodeToCommandBuffer:sourceArray:resultState:destinationArray:")]
		MPSNDArray EncodeToCommandBuffer (IMTLCommandBuffer commandBuffer, MPSNDArray sourceArray, [NullAllowed] MPSState gradientState, MPSNDArray destinationArray);
	}

	[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
	[BaseType (typeof (MPSNDArrayUnaryKernel))]
	[DisableDefaultCtor]
	interface MPSNDArrayIdentity {
		[Export ("initWithDevice:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IMTLDevice device);

		[Export ("reshapeWithCommandBuffer:sourceArray:shape:destinationArray:")]
		[return: NullAllowed]
		MPSNDArray Reshape ([NullAllowed] IMTLCommandBuffer commandBuffer, MPSNDArray sourceArray, [BindAs (typeof (int []))] NSNumber [] shape, [NullAllowed] MPSNDArray destinationArray);

		[Export ("reshapeWithCommandBuffer:sourceArray:dimensionCount:dimensionSizes:destinationArray:")]
		[return: NullAllowed]
		[Internal]
		MPSNDArray _Reshape ([NullAllowed] IMTLCommandBuffer commandBuffer, MPSNDArray sourceArray, nuint numberOfDimensions, /* NSUInteger */ IntPtr dimensionSizes, [NullAllowed] MPSNDArray destinationArray);

		[Export ("reshapeWithCommandEncoder:commandBuffer:sourceArray:shape:destinationArray:")]
		[return: NullAllowed]
		MPSNDArray Reshape ([NullAllowed] IMTLComputeCommandEncoder encoder, [NullAllowed] IMTLCommandBuffer commandBuffer, MPSNDArray sourceArray, [BindAs (typeof (int []))] NSNumber [] shape, [NullAllowed] MPSNDArray destinationArray);

		[Export ("reshapeWithCommandEncoder:commandBuffer:sourceArray:dimensionCount:dimensionSizes:destinationArray:")]
		[return: NullAllowed]
		[Internal]
		MPSNDArray _Reshape ([NullAllowed] IMTLComputeCommandEncoder encoder, [NullAllowed] IMTLCommandBuffer commandBuffer, MPSNDArray sourceArray, nuint numberOfDimensions, /* NSUInteger */ IntPtr dimensionSizes, [NullAllowed] MPSNDArray destinationArray);
	}

	[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
	[Native]
	public enum MPSNDArrayQuantizationScheme : ulong {
		None = 0,
		Affine,
		Lut,
	}

	[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
	[BaseType (typeof (NSObject))]
	[DefaultCtorVisibility (Visibility.Protected)]
	interface MPSNDArrayQuantizationDescriptor : NSCopying {
		[Export ("quantizationDataType")]
		MPSDataType QuantizationDataType { get; }

		[Export ("quantizationScheme")]
		MPSNDArrayQuantizationScheme QuantizationScheme { get; }
	}

	[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
	[BaseType (typeof (MPSNDArrayQuantizationDescriptor))]
	interface MPSNDArrayAffineQuantizationDescriptor {
		[Export ("hasZeroPoint")]
		bool HasZeroPoint { get; set; }

		[Export ("hasMinValue")]
		bool HasMinValue { get; set; }

		[Export ("implicitZeroPoint")]
		bool ImplicitZeroPoint { get; set; }

		[Export ("initWithDataType:hasZeroPoint:hasMinValue:")]
		NativeHandle Constructor (MPSDataType quantizationDataType, bool hasZeroPoint, bool hasMinValue);
	}

	[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
	[BaseType (typeof (MPSNDArrayQuantizationDescriptor), Name = "MPSNDArrayLUTQuantizationDescriptor")]
	[DisableDefaultCtor]
	interface MPSNDArrayLutQuantizationDescriptor {
		[Export ("initWithDataType:")]
		NativeHandle Constructor (MPSDataType quantizationDataType);

		[Export ("initWithDataType:vectorAxis:")]
		NativeHandle Constructor (MPSDataType quantizationDataType, nuint vectorAxis);
	}

	[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
	[BaseType (typeof (MPSNDArrayMatrixMultiplication))]
	[DisableDefaultCtor]
	interface MPSNDArrayQuantizedMatrixMultiplication {
		[Export ("initWithDevice:leftQuantizationDescriptor:rightQuantizationDescriptor:")]
		NativeHandle Constructor (IMTLDevice device, [NullAllowed] MPSNDArrayQuantizationDescriptor leftQuantizationDescriptor, [NullAllowed] MPSNDArrayQuantizationDescriptor rightQuantizationDescriptor);
	}

	[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
	[BaseType (typeof (MPSNDArrayMultiaryKernel), Name = "MPSNDArrayLUTDequantize")]
	[DisableDefaultCtor]
	interface MPSNDArrayLutDequantize {
		[DesignatedInitializer]
		[Export ("initWithDevice:")]
		NativeHandle Constructor (IMTLDevice device);
	}

	[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
	[BaseType (typeof (MPSNDArrayMultiaryKernel), Name = "MPSNDArrayVectorLUTDequantize")]
	[DisableDefaultCtor]
	interface MPSNDArrayVectorLutDequantize {
		[Export ("vectorAxis")]
		nuint VectorAxis { get; set; }

		[DesignatedInitializer]
		[Export ("initWithDevice:axis:")]
		NativeHandle Constructor (IMTLDevice device, nuint axis);
	}

	[TV (18, 0), Mac (15, 0), iOS (18, 0), MacCatalyst (18, 0)]
	[BaseType (typeof (MPSNDArrayMultiaryKernel))]
	[DisableDefaultCtor]
	interface MPSNDArrayAffineInt4Dequantize {
		[Export ("initWithDevice:quantizationDescriptor:")]
		NativeHandle Constructor (IMTLDevice device, MPSNDArrayAffineQuantizationDescriptor quantizationDescriptor);
	}
}
