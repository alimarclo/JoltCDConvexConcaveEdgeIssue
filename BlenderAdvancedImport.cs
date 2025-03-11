using Godot;
using Godot.Collections;
using System;
using System.Linq;
using System.Reflection;

[Tool]
public partial class BlenderAdvancedImport : EditorScenePostImport
{

    public override GodotObject _PostImport(Node scene)
    {
        // By design, this just gets the first model to avoid any needed intermediate manual process of extracting each body that's needed
        var model = scene.GetChild<Node3D>(0);
        string modelName = model.Name;
        string name;

        if(modelName.EndsWith("-nobody")) {
            var collisionShape = model.GetChild<CollisionShape3D>(0);
            model.RemoveChild(collisionShape);
            collisionShape.Owner = null;
            scene.RemoveChild(model);
            model.Owner = null;
            scene.Free();
            model.Free();

            name = modelName.Substring(0, modelName.Length-"-nobody".Length);
            collisionShape.Name = name;

            return collisionShape;
        }

        PhysicsBody3D body;
        var staticBody = model.GetChild<StaticBody3D>(0);

        model.RemoveChild(staticBody);
        staticBody.Owner = null;
        scene.RemoveChild(model);
        model.Owner = null;
        scene.Free();
        staticBody.AddChild(model);
        model.Owner = staticBody;

        staticBody.Transform = model.Transform;

        var modelBasis = new Basis(
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 1));

        model.Transform = new Transform3D(modelBasis, Vector3.Zero);

        if(modelName.EndsWith("-ani")) {
            name = modelName.Substring(0, modelName.Length-"-ani".Length);
            model.Name = name;

            var animatedBody3D = Replace<StaticBody3D, AnimatableBody3D>(staticBody);
            animatedBody3D.SyncToPhysics = false;
            body = animatedBody3D;
        } else if(modelName.EndsWith("-rigid")) {
            name = modelName.Substring(0, modelName.Length-"-rigid".Length);
            model.Name = name;

            var rigidBody3D = Replace<PhysicsBody3D, RigidBody3D>(staticBody);
            body = rigidBody3D;
        } else {
            name = modelName;
            body = staticBody;
        }

        body.Name = name;

        return body;
    }

    private TReplaceNode Replace<TOriganalNode, TReplaceNode>(TOriganalNode original)
            where TOriganalNode : Node
            where TReplaceNode : TOriganalNode {
        
        var replaceNode = Activator.CreateInstance<TReplaceNode>();

        foreach(var child in original.GetChildren()) {
            original.RemoveChild(child);
            child.Owner = null;
            replaceNode.AddChild(child);
            child.Owner = replaceNode;
        }
        
        var originalProps = typeof(TOriganalNode)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.CanWrite)
            .Where(p => !p.Name.StartsWith("Global"))
            .ToList();
        
        foreach(var originalProp in originalProps) {
            originalProp.SetValue(replaceNode, originalProp.GetValue(original));
        }

        original.Free();

        return replaceNode;
    }


}
