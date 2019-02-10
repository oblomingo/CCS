export function settingsIsLoading(state = false, action) {
    switch (action.type) {
        case 'SETTINGS_IS_LOADING':
            return action.isLoading;

        default:
            return state;
    }
}

export function settingsFetchDataSuccess(state = { mode: 0, isOn: false }, action) {
    switch (action.type) {
        case 'SETTING_FETCH_DATA_SUCCESS':
            return action.settings;

        default:
            return state;
    }
}

export function settingsHasErrored(state = false, action) {
    switch (action.type) {
        case 'SETTINGS_HAS_ERRORED':
            return action.hasErrored;

        default:
            return state;
    }
}

export function settingsIsSaving(state = false, action) {
    switch (action.type) {
        case 'SETTINGS_IS_SAVING':
            return action.isSaving;

        default:
            return state;
    }
}

export function settingsChanged(state = { mode: 0, isOn: false }, action) {
    switch (action.type) {
        case 'SETTINGS_CHANGED':
            return action.settings;

        default:
            return state;
    }
}