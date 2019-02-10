export function settingsIsLoading(bool) {
    return {
        type: 'SETTINGS_IS_LOADING',
        isLoading: bool
    };
}

export function settingsFetchDataSuccess(settings) {
    return {
        type: 'SETTING_FETCH_DATA_SUCCESS',
        settings
    };
}

export function settingsHasErrored(bool) {
    return {
        type: 'SETTINGS_HAS_ERRORED',
        hasErrored: bool
    };
}
export function settingsIsSaving(bool) {
    return {
        type: 'SETTINGS_IS_SAVING',
        isSaving: bool
    };
}
export function settingsChanged(settings) {
    return {
        type: 'SETTINGS_CHANGED',
        settings
    };
}

export function settingsFetchData(url) {
    return (dispatch) => {
        dispatch(settingsIsLoading(true));

        fetch(url)
            .then((response) => {
                if (!response.ok) {
                    throw Error(response.statusText);
                }

                dispatch(settingsIsLoading(false));

                return response;
            })
            .then((response) => response.json())
            .then((settings) => dispatch(settingsChanged(settings)))
            .catch(() => dispatch(settingsHasErrored(true)));
    };
}

export function postSettings(url, settings) {
    return (dispatch) => {
		dispatch(settingsIsSaving(true));

		fetch(url, {
			method: 'POST',
			headers: {
				'Accept': 'application/json',
    			'Content-Type': 'application/json',
			},
			body: JSON.stringify(settings)
		})
            .then((response) => {
                if (!response.ok) {
                    throw Error(response.statusText);
                }

                dispatch(settingsIsSaving(false));

                return response;
            })
            .then((response) => response.json())
            .then((settings) => dispatch(settingsChanged(settings)))
            .catch(() => dispatch(settingsHasErrored(true)));
    };
}