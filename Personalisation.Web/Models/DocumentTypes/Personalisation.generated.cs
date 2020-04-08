//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder v8.5.3
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder.Embedded;

namespace Personalisation.Web.Models.DocumentTypes
{
	// Mixin Content Type with alias "personalisation"
	/// <summary>Personalisation</summary>
	public partial interface IPersonalisation : IPublishedContent
	{
		/// <summary>Tags</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		global::System.Collections.Generic.IEnumerable<string> Tags { get; }
	}

	/// <summary>Personalisation</summary>
	[PublishedModel("personalisation")]
	public partial class Personalisation : PublishedContentModel, IPersonalisation
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		public new const string ModelTypeAlias = "personalisation";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		public new static IPublishedContentType GetModelContentType()
			=> PublishedModelUtility.GetModelContentType(ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Personalisation, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(), selector);
#pragma warning restore 0109

		// ctor
		public Personalisation(IPublishedContent content)
			: base(content)
		{ }

		// properties

		///<summary>
		/// Tags
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		[ImplementPropertyType("tags")]
		public global::System.Collections.Generic.IEnumerable<string> Tags => GetTags(this);

		/// <summary>Static getter for Tags</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder", "8.5.3")]
		public static global::System.Collections.Generic.IEnumerable<string> GetTags(IPersonalisation that) => that.Value<global::System.Collections.Generic.IEnumerable<string>>("tags");	
	}
}
