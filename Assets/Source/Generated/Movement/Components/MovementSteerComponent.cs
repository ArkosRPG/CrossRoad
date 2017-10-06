//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class MovementEntity {

    public SteerComponent steer { get { return (SteerComponent)GetComponent(MovementComponentsLookup.Steer); } }
    public bool hasSteer { get { return HasComponent(MovementComponentsLookup.Steer); } }

    public void AddSteer(float newValue) {
        var index = MovementComponentsLookup.Steer;
        var component = CreateComponent<SteerComponent>(index);
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceSteer(float newValue) {
        var index = MovementComponentsLookup.Steer;
        var component = CreateComponent<SteerComponent>(index);
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveSteer() {
        RemoveComponent(MovementComponentsLookup.Steer);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class MovementMatcher {

    static Entitas.IMatcher<MovementEntity> _matcherSteer;

    public static Entitas.IMatcher<MovementEntity> Steer {
        get {
            if (_matcherSteer == null) {
                var matcher = (Entitas.Matcher<MovementEntity>)Entitas.Matcher<MovementEntity>.AllOf(MovementComponentsLookup.Steer);
                matcher.componentNames = MovementComponentsLookup.componentNames;
                _matcherSteer = matcher;
            }

            return _matcherSteer;
        }
    }
}
