using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using ImpossibleOdds.DependencyInjection;

public class DependenceInjectionSceneScope : AbstractDependencyScopeBehaviour
{
    public override void Inject()
    {
        ContainerInstance = (DependencyContainer) DependencyContainer;
        gameObject.scene.GetRootGameObjects().Inject(DependencyContainer, true);
    }

    public static DependencyContainer ContainerInstance { get; private set; }

    protected override void InstallBindings()
    {
        GameObject[] rootGameObjects = gameObject.scene.GetRootGameObjects();
        foreach(GameObject rootGameObject in rootGameObjects)
        {
            IDependencyScopeInstaller[] installers = rootGameObject.GetComponentsInChildren<IDependencyScopeInstaller>();
            foreach (IDependencyScopeInstaller installer in installers)
            {
                installer.Install(container);
            }
        }

    }
}
