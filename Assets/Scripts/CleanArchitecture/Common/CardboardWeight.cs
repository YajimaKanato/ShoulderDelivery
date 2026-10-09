using UnityEngine;

namespace ShoulderDerivery.Common
{
    public enum CardboardWeight
    {
        [InspectorName("なんでもいい")] NoRequested = -1,
        [InspectorName("500g")] Weight0 = 500,
        [InspectorName("1000g")] Weight1 = 1000,
        [InspectorName("1500g")] Weight2 = 1500,
        [InspectorName("2000g")] Weight3 = 2000,
        [InspectorName("2500g")] Weight4 = 2500,
        [InspectorName("3000g")] Weight5 = 3000
    }
}
