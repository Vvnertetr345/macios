// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.CodeAnalysis;
using Microsoft.Macios.Generator.DataModel;
using Serilog;

namespace Microsoft.Macios.Transformer.Workers;

class CategoryTransformer (string destinationDirectory) : ITransformer<(string Path, Binding Binding)> {

	readonly static ILogger logger = Log.ForContext<CategoryTransformer> ();
	public bool UseBackgroundThread { get => true; }

	public Task ConsumeAsync ((string Path, Binding Binding) message, CancellationToken token = new ())
	{
		logger.Information ("Transforming {SymbolName} for path {Path} to {DestinationDirectory}",
			message.Binding.FullyQualifiedSymbol, message.Path, destinationDirectory);
		return Task.Delay (10);
	}

	public Task ConsumeAsync ((string Path, Binding Binding) message, Exception exception,
		CancellationToken token = new CancellationToken ())
	{
		logger.Error (exception, "Error transforming {SymbolName} for path {Path} to {DestinationDirectory}:",
			message.Binding.FullyQualifiedSymbol, message.Path, destinationDirectory);
		return Task.CompletedTask;
	}

	public (string Path, Binding Binding) CreateMessage (SyntaxTree treeNode, Binding binding)
	{
		return (treeNode.FilePath, binding);
	}

	public void Dispose () { }

	public ValueTask DisposeAsync () => ValueTask.CompletedTask;


}
