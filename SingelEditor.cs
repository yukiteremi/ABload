using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbManager : MonoBehaviour
{
    public void loadAb()
    {
        //1. 加载ab包
        AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "firstab");
        //2. 非泛型加载资源,对于同名不同类型的情况可以指定类型进行加载
        GameObject go = ab.LoadAsset("Cube", typeof(GameObject)) as GameObject;
        //3. 泛型加载资源
        GameObject go2 = ab.LoadAsset<GameObject>("Cube");
        //4. 创建资源
        Instantiate(go2);
        //释放ab包，参数true为释放ab包的同时删除所有和ab包相关的资源；
        //false为只释放ab包
        ab.Unload(false);
        //释放所有ab包，参数true为释放ab包的同时删除所有和ab包相关的资源；
        //false为只释放ab包
        AssetBundle.UnloadAllAssetBundles(false);
    }

    /// <summary>
    /// 由于协程的特殊性，所以我们用一个函数来接受来自协程的结果
    /// </summary>
    /// <param name="abr">异步加载完成后的资源</param>
    private void AsyncHandler(AssetBundleRequest abr)
    {
        print(abr.asset.name);
    }
    /// <summary>
    /// 异步加载AB包
    /// </summary>
    /// <param name="func">接受来自协程的结果的函数，默认不传</param>
    private IEnumerator LoadAsync(Action<AssetBundleRequest> func = null)
    {
        //异步加载AB包的类型不同于同步加载
        AssetBundleCreateRequest ab = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + "firstab");
        yield return ab;
        //异步加载的资源类型不同于同步加载
        AssetBundleRequest abr = ab.assetBundle.LoadAssetAsync<GameObject>("Cube");
        yield return abr;
        //如果函数不为空则调用
        func?.Invoke(abr);
    }
}