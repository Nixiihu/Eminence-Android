using UnityEngine;
using UnityEngine.UI;
using EminenceTest;

public class EminenceDemoBootstrap : MonoBehaviour
{
    private EminenceTestController controller;
    private EminenceAimAssist aim;
    private EminenceNoRecoil recoil;
    private EminenceLeadAim lead;
    private EminenceWeaponSwitchTest switchTest;
    private Text status;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        var cameraObject = new GameObject("Eminence Camera");
        var cam = cameraObject.AddComponent<Camera>();
        cameraObject.tag = "MainCamera";
        cam.transform.position = new Vector3(0, 1.6f, -8f);
        cam.transform.rotation = Quaternion.identity;

        var root = new GameObject("Eminence Test Controller");
        controller = root.AddComponent<EminenceTestController>();
        aim = root.AddComponent<EminenceAimAssist>();
        recoil = root.AddComponent<EminenceNoRecoil>();
        lead = root.AddComponent<EminenceLeadAim>();
        switchTest = root.AddComponent<EminenceWeaponSwitchTest>();
        var details = root.AddComponent<EminenceDetails>();
        var safe = root.AddComponent<EminenceSafeBootstrap>();

        aim.aimCamera = cam;
        recoil.cameraTransform = cam.transform;
        controller.aimAssist = aim;
        controller.noRecoil = recoil;
        controller.leadAim = lead;
        controller.weaponSwitch = switchTest;

        // A harmless target for development-only aim/lead calculations.
        var target = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        target.name = "Test Target";
        target.transform.position = new Vector3(0, 1.6f, 8f);
        aim.targetBody = target.transform;
        aim.targetNeck = target.transform;
        aim.targetHead = target.transform;

        BuildUI(details);
    }

    private void BuildUI(EminenceDetails details)
    {
        var canvasObject = new GameObject("Eminence UI");
        var canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasObject.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1080, 1920);
        canvasObject.AddComponent<GraphicRaycaster>();

        var panel = new GameObject("Panel");
        panel.transform.SetParent(canvasObject.transform, false);
        var image = panel.AddComponent<Image>();
        image.color = new Color(0.07f, 0.035f, 0.12f, 0.94f);
        var rect = panel.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.06f, 0.08f);
        rect.anchorMax = new Vector2(0.94f, 0.92f);
        rect.offsetMin = rect.offsetMax = Vector2.zero;

        status = CreateText(panel.transform, "EMINENCE\nUnity Development Test Harness\n\nProfile: LEGIT\nModules: OFF\nFPS: --", 44, TextAnchor.UpperLeft);
        var statusRect = status.rectTransform;
        statusRect.anchorMin = new Vector2(0.06f, 0.45f);
        statusRect.anchorMax = new Vector2(0.94f, 0.94f);
        statusRect.offsetMin = statusRect.offsetMax = Vector2.zero;

        CreateButton(panel.transform, "LEGIT", new Vector2(0.08f, 0.31f), () => { controller.SetProfile(TestProfile.Legit); UpdateStatus(details); });
        CreateButton(panel.transform, "RAGE TEST", new Vector2(0.52f, 0.31f), () => { controller.SetProfile(TestProfile.Rage); UpdateStatus(details); });
        CreateButton(panel.transform, "FLAG TEST", new Vector2(0.08f, 0.20f), () => { controller.SimulateDetectedModule("ManualTest"); UpdateStatus(details); });
        CreateButton(panel.transform, "FAST SWITCH TEST", new Vector2(0.52f, 0.20f), () => { controller.RunWeaponSwitchTest(); UpdateStatus(details); });
        CreateButton(panel.transform, "AIM ON/OFF", new Vector2(0.08f, 0.09f), () => { aim.settings.enabled = !aim.settings.enabled; UpdateStatus(details); });
        CreateButton(panel.transform, "RECOIL ON/OFF", new Vector2(0.52f, 0.09f), () => { recoil.enabledForTest = !recoil.enabledForTest; UpdateStatus(details); });
    }

    private Text CreateText(Transform parent, string value, int size, TextAnchor anchor)
    {
        var go = new GameObject("Text");
        go.transform.SetParent(parent, false);
        var t = go.AddComponent<Text>();
        t.text = value;
        t.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        t.fontSize = size;
        t.alignment = anchor;
        t.color = Color.white;
        return t;
    }

    private void CreateButton(Transform parent, string label, Vector2 anchor, UnityEngine.Events.UnityAction action)
    {
        var go = new GameObject(label);
        go.transform.SetParent(parent, false);
        var image = go.AddComponent<Image>();
        image.color = new Color(0.32f, 0.12f, 0.55f, 0.95f);
        var button = go.AddComponent<Button>();
        button.onClick.AddListener(action);
        var rect = go.GetComponent<RectTransform>();
        rect.anchorMin = anchor;
        rect.anchorMax = anchor + new Vector2(0.38f, 0.075f);
        rect.offsetMin = rect.offsetMax = Vector2.zero;
        var text = CreateText(go.transform, label, 28, TextAnchor.MiddleCenter);
        var tr = text.rectTransform;
        tr.anchorMin = Vector2.zero; tr.anchorMax = Vector2.one; tr.offsetMin = tr.offsetMax = Vector2.zero;
    }

    private void UpdateStatus(EminenceDetails details)
    {
        if (status == null) return;
        status.text = "EMINENCE\nUnity Development Test Harness\n\nProfile: " + controller.profile +
                      "\nAim: " + (aim.settings.enabled ? "ON" : "OFF") +
                      "\nNo Recoil: " + (recoil.enabledForTest ? "ON" : "OFF") +
                      "\nFPS: " + details.FPS.ToString("0");
    }

    private void Update()
    {
        if (status != null && controller != null)
        {
            var details = controller.GetComponent<EminenceDetails>();
            if (details != null && Time.frameCount % 15 == 0) UpdateStatus(details);
        }
    }
}
