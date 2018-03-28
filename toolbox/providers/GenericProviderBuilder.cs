using System;

namespace com.pscalderonm.toolbox.providers
{
	public class GenericProviderBuilder
	{
		public static ProviderBuilder<I, O> Of<I, O>() where I : O where O : class => new ProviderBuilder<I, O>();

		public class ProviderBuilder<I, O> where I : O where O : class
		{
			private object[] arguments;

			public ProviderBuilder<I, O> Arguments(params object[] args)
			{
				arguments = args;
				return this;
			}

			public O Create()
			{
				return Activator.CreateInstance(typeof(I), arguments) as O;
			}
		}
	}	
}
