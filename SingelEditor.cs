using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbManager : MonoBehaviour
{
    public void loadAb()
    {
        //1. ����ab��
        AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "firstab");
        //2. �Ƿ��ͼ�����Դ,����ͬ����ͬ���͵��������ָ�����ͽ��м���
        GameObject go = ab.LoadAsset("Cube", typeof(GameObject)) as GameObject;
        //3. ���ͼ�����Դ
        GameObject go2 = ab.LoadAsset<GameObject>("Cube");
        //4. ������Դ
        Instantiate(go2);
        //�ͷ�ab��������trueΪ�ͷ�ab����ͬʱɾ�����к�ab����ص���Դ��
        //falseΪֻ�ͷ�ab��
        ab.Unload(false);
        //�ͷ�����ab��������trueΪ�ͷ�ab����ͬʱɾ�����к�ab����ص���Դ��
        //falseΪֻ�ͷ�ab��
        AssetBundle.UnloadAllAssetBundles(false);
    }

    /// <summary>
    /// ����Э�̵������ԣ�����������һ����������������Э�̵Ľ��
    /// </summary>
    /// <param name="abr">�첽������ɺ����Դ</param>
    private void AsyncHandler(AssetBundleRequest abr)
    {
        print(abr.asset.name);
    }
    /// <summary>
    /// �첽����AB��
    /// </summary>
    /// <param name="func">��������Э�̵Ľ���ĺ�����Ĭ�ϲ���</param>
    private IEnumerator LoadAsync(Action<AssetBundleRequest> func = null)
    {
        //�첽����AB�������Ͳ�ͬ��ͬ������
        AssetBundleCreateRequest ab = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + "firstab");
        yield return ab;
        //�첽���ص���Դ���Ͳ�ͬ��ͬ������
        AssetBundleRequest abr = ab.assetBundle.LoadAssetAsync<GameObject>("Cube");
        yield return abr;
        //���������Ϊ�������
        func?.Invoke(abr);
    }
}