export function dashboardDataHasErrored(state = false, action) {
    switch (action.type) {
        case 'DASHBOARD_DATA_HAS_ERRORED':
            return action.hasErrored;

        default:
            return state;
    }
}

export function dashboardDataIsLoading(state = false, action) {
    switch (action.type) {
        case 'DASHBOARD_DATA_IS_LOADING':
            return action.isLoading;

        default:
            return state;
    }
}

export function dashboardDataFetchDataSuccess(state = { settings: {}}, action) {
    switch (action.type) {
        case 'DASHBOARD_DATA_FETCH_DATA_SUCCESS':
            return action.dashboardData;

        default:
            return state;
    }
}