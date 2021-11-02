import React from 'react';
import Typography from '@mui/material/Typography';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import Switch from '@mui/material/Switch';

export class Dashboard extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            dashboardData: {},
            settings: {
                mode: 0,
                isOn: false
            },
            isLoading: false,
            hasErrored: false,
            error: ''
        };
    }

    componentDidMount() {
        this.getDashboardData();
        this.getSettings();
    }

    handleModeChange(reactNode) {
        this.setSettingsToStateAndUpdate({ ...this.state.settings, mode: reactNode.props.value });
    }

    handleSwitchChange(isOn) {
        this.setSettingsToStateAndUpdate({ ...this.state.settings, isOn: isOn });
    }

    setSettingsToStateAndUpdate(settings) {
        this.setState({ settings: settings });
        this.updateSettings(settings);
    }

    render() {
        let temperature = this.state.dashboardData.temperature
            ? this.state.dashboardData.temperature.toLocaleString(undefined, { maximumFractionDigits: 2 })
            : 0;

        let humidity = this.state.dashboardData.humidity
            ? this.state.dashboardData.humidity.toLocaleString(undefined, { maximumFractionDigits: 2 })
            : 0;

        if (this.state.hasErrored) {
            return <p>Sorry! There was an error: { this.state.error }</p>;
        }

        if (this.state.isLoading) {
            return <p>Loading…</p>;
        }

        return (
            <React.Fragment>
                <Typography variant="h6" gutterBottom>
                    Dashboard
                </Typography>
                <Typography component={'span'} variant="body1" gutterBottom>
                    <div className="Dashboard">
                        <div>Sensors &amp; Relays</div>
                        <div>Temperature:&nbsp;{temperature}&nbsp;&#8451;</div>
                        <div>Humidity:&nbsp;{humidity}&nbsp;%</div>
                        <div>Is Relay On:&nbsp;{String(this.state.dashboardData.isOn)}</div>
                        <br />
                    </div>
                </Typography>
                <FormControl variant="outlined" className="formControl input-mode">
                    <InputLabel id="simple-select-label">Mode</InputLabel>
                    <Select
                        value={this.state.settings.mode}
                        onChange={(_event, reactNode) => this.handleModeChange(reactNode)}
                    >
                        <MenuItem value={0}>Manual</MenuItem>
                        <MenuItem value={1}>Automatic</MenuItem>
                    </Select>
                </FormControl>
                {
                    this.state.settings.mode === 0 ?
                        <div>
                            <div>Toggle relay</div>
                            <Switch
                                name="isOn"
                                checked={this.state.settings.isOn}
                                onChange={(_event, isOn) => this.handleSwitchChange(isOn)}
                            />
                        </div>
                        : null
                }
            </React.Fragment>
        );
    }

    async getDashboardData() {
        this.setState({ ...this.state, loading: true });
        const response = await fetch('api/Dashboard');
        const data = await response.json();
        this.setState({ ...this.state, dashboardData: data, loading: false });
    }

    async getSettings() {
        this.setState({ ...this.state, loading: true });
        const response = await fetch('api/Settings');
        const data = await response.json();
        this.setState({ ...this.state, settings: data, loading: false });
    }

    async updateSettings(settings) {
        const response = await fetch('api/Settings', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(settings)
        })
        if (!response.ok) {
            this.setState({ ...this.state, hasErrored: true, error: 'Invalid settings update.' });
        }
    }
}
