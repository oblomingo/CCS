export function chartDataHasErrored(state = false, action) {
    switch (action.type) {
        case 'CHART_DATA_HAS_ERRORED':
            return action.hasErrored;

        default:
            return state;
    }
}

export function chartDataIsLoading(state = false, action) {
    switch (action.type) {
        case 'CHART_DATA_IS_LOADING':
            return action.isLoading;

        default:
            return state;
    }
}

export function chartDataFetchDataSuccess(state = [], action) {
    switch (action.type) {
        case 'CHART_DATA_FETCH_DATA_SUCCESS':
            return action.chartData;

        default:
            return state;
    }
}