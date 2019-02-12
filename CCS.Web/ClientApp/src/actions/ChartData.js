export function chartDataHasErrored(bool) {
    return {
        type: 'CHART_DATA_HAS_ERRORED',
        hasErrored: bool
    };
}
export function chartDataIsLoading(bool) {
    return {
        type: 'CHART_DATA_IS_LOADING',
        isLoading: bool
    };
}
export function chartDataFetchDataSuccess(chartData) {
    return {
        type: 'CHART_DATA_FETCH_DATA_SUCCESS',
        chartData
    };
}

export function chartDataFetchData(url) {
    return (dispatch) => {
        dispatch(chartDataIsLoading(true));

        fetch(url)
            .then((response) => {
                if (!response.ok) {
                    throw Error(response.statusText);
                }

                dispatch(chartDataIsLoading(false));

                return response;
            })
            .then((response) => response.json())
            .then((chartData) => dispatch(chartDataFetchDataSuccess(chartData)))
            .catch(() => dispatch(chartDataHasErrored(true)));
    };
}