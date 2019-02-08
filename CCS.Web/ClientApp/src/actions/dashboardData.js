export function dashboardDataHasErrored(bool) {
    return {
        type: 'DASHBOARD_DATA_HAS_ERRORED',
        hasErrored: bool
    };
}
export function dashboardDataIsLoading(bool) {
    return {
        type: 'DASHBOARD_DATA_IS_LOADING',
        isLoading: bool
    };
}
export function dashboardDataFetchDataSuccess(dashboardData) {
    return {
        type: 'DASHBOARD_DATA_FETCH_DATA_SUCCESS',
        dashboardData
    };
}

export function dashboardDataFetchData(url) {
    return (dispatch) => {
        dispatch(dashboardDataIsLoading(true));

        fetch(url)
            .then((response) => {
                if (!response.ok) {
                    throw Error(response.statusText);
                }

                dispatch(dashboardDataIsLoading(false));

                return response;
            })
            .then((response) => response.json())
            .then((dashboardData) => dispatch(dashboardDataFetchDataSuccess(dashboardData)))
            .catch(() => dispatch(dashboardDataHasErrored(true)));
    };
}