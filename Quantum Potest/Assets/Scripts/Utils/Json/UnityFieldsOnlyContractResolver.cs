using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

public class UnityFieldsOnlyContractResolver : DefaultContractResolver {
	protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization) {
		JsonProperty property = base.CreateProperty(member, memberSerialization);

		if (member.DeclaringType != null && member.DeclaringType.Assembly.FullName.Contains("UnityEngine")) {
			if (member.MemberType == MemberTypes.Property) {
				property.Ignored = true;
			}
		}
		return property;
	}
}
