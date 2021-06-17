using System;
using UnityEditor;
using UnityEditor.SettingsManagement;

public class SerializableSetting : IUserSetting {



    public string key => throw new NotImplementedException();

    public Type type => throw new NotImplementedException();

    public SettingsScope scope => throw new NotImplementedException();

    public string settingsRepositoryName => throw new NotImplementedException();

    public Settings settings => throw new NotImplementedException();

    public void ApplyModifiedProperties() => throw new NotImplementedException();
    public void Delete(bool saveProjectSettingsImmediately = false) => throw new NotImplementedException();
    public object GetDefaultValue() => throw new NotImplementedException();
    public object GetValue() => throw new NotImplementedException();
    public void Reset(bool saveProjectSettingsImmediately = false) => throw new NotImplementedException();
    public void SetValue(object value, bool saveProjectSettingsImmediately = false) => throw new NotImplementedException();
}
