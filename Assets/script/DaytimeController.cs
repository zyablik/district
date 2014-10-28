using UnityEngine;
using System.Collections;

[System.Serializable]
public class RenderSettingsObject {
	public bool fog;
	public Color fogColor;
	public FogMode fogMode;
	public float fogDensity;
	public float fogStartDistance;
	public float fogEndDistance;
	public Color ambientLight;
}

public class DaytimeController : MonoBehaviour {
	public enum Daytime { Day, Night }
	public Daytime daytime;

	public RenderSettingsObject day_settings;
	public RenderSettingsObject night_settings;

	private RenderSettingsObject render_settings;

	void Start() {
		applyLight();
	}

	public void applyLight() {
		GameObject[] night_lights = GameObject.FindGameObjectsWithTag("NightLight");
		foreach(GameObject night_light in night_lights) {
			night_light.light.enabled = (daytime == Daytime.Night);
		}

		GameObject[] day_lights = GameObject.FindGameObjectsWithTag("DayLight");
		foreach(GameObject day_light in day_lights) {
			day_light.light.enabled = (daytime == Daytime.Day);
		}

		if(daytime == Daytime.Night) {
			render_settings = night_settings;
		} else {
			render_settings = day_settings;
		}
		RenderSettings.fog = render_settings.fog;
		RenderSettings.fogColor = render_settings.fogColor;
		RenderSettings.fogMode = render_settings.fogMode;
		RenderSettings.fogDensity = render_settings.fogDensity;
		RenderSettings.fogStartDistance = render_settings.fogStartDistance;
		RenderSettings.fogEndDistance = render_settings.fogEndDistance;
		RenderSettings.ambientLight = render_settings.ambientLight;

	}
}
