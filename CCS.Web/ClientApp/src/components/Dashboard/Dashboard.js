import React, {Component} from 'react';
import PropTypes from 'prop-types';
import {connect} from 'react-redux';
import {dashboardDataFetchData} from '../../actions/dashboardData';
import {settingsFetchData, postSettings} from '../../actions/Settings';
import Typography from '@material-ui/core/Typography';
import {withStyles} from '@material-ui/core/styles';
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import OutlinedInput from '@material-ui/core/OutlinedInput';
import InputLabel from '@material-ui/core/InputLabel';
import FormControl from '@material-ui/core/FormControl';
import ReactDOM from 'react-dom';
import Switch from '@material-ui/core/Switch';
import TextField from '@material-ui/core/TextField';

const styles = theme => ({
  root: {
    width: '100%',
    maxWidth: 500,
    marginTop: 84,
    marginLeft: 40
  },
  formControl: {
    margin: theme.spacing.unit,
    minWidth: 150,
    marginLeft: -2
  },
  selectEmpty: {
    marginTop: theme.spacing.unit * 2
  }
});

class Dashboard extends Component {

  state = {
    mode: '',
    name: 'hai',
    labelWidth: 0,
    isOn: true,
    settings: {
      mode: 0,
      innerTemperatureMin: 0,
      IsOn: false
    }
  };

  componentDidMount() {
    this.props.fetchData('/api/Dashboard');
    this.props.fetchSettings('/api/Settings');

      this.setState({
        labelWidth: ReactDOM.findDOMNode(this.InputLabelRef).offsetWidth,
      });
  }

  handleSettingsChange = event => {
    this.props.saveSettings('api/Settings', { ...this.props.settings, [event.target.name]: event.target.value});
  };

  handleSwitchChange = event => {
      this.props.saveSettings('api/Settings', { ...this.props.settings, isOn: event.target.checked});
  };

  render() {
    if (this.props.hasErrored) {
      return <p>Sorry! There was an error loading the items</p>;
    }

    if (this.props.isLoading) {
      return <p>Loading…</p>;
    }

    const {classes} = this.props;

    return (
      <div className={classes.root}>
        <Typography variant="h6" gutterBottom>
          Dashboard
        </Typography>
        <Typography variant="body1" gutterBottom>
          <div className="Dashboard">
            <div>Sensors &amp; Relays</div>
            <div>Temperature:{this.props.dashboardData.temperature}</div>
            <div>Humidity:{this.props.dashboardData.humidity}</div>
            <div>Is Relay On:{String(this.props.dashboardData.isOn)}</div>
            <br/>
          </div>
        </Typography>
        <FormControl variant="outlined" className={classes.formControl}>
          <InputLabel
            ref={ref => {
            this.InputLabelRef = ref;
          }}
            htmlFor="outlined-mode-simple">
            Mode
          </InputLabel>
          <Select
            value={this.props.settings.mode}
            onChange={this.handleSettingsChange}
            input={< OutlinedInput labelWidth = { this.state.labelWidth }
            name="mode" 
            id="outlined-mode-simple" />}>
            <MenuItem value={0}>Manual</MenuItem>
            <MenuItem value={1}>Automatic</MenuItem>
          </Select>
        </FormControl>
        {
          this.props.settings.mode === 0 ?
            <div>
              <div>Toggle relay</div>
              <Switch
                name="isOn"
                checked={this.props.settings.isOn}
                onChange={this.handleSwitchChange}
              />
            </div>
          : null
        }
        {
          this.props.settings.mode === 1 ?
            <div>
              <TextField
            id="outlined-number"
            name="innerTemperatureMin"
            label="Indoor temperature minimum"
            value={this.props.settings.innerTemperatureMin}
            type="number"
            className={classes.textField}
            InputLabelProps={{
              shrink: true,
            }}
            margin="normal"
            variant="outlined"
            onChange={this.handleSettingsChange}
          />
            </div>
          : null
        }

      </div>
    );
  }
}

Dashboard.propTypes = {
  fetchData: PropTypes.func.isRequired,
  dashboardData: PropTypes.object.isRequired,
  hasErrored: PropTypes.bool,
  isLoading: PropTypes.bool,
  settings: PropTypes.object,
  classes: PropTypes.object.isRequired
};

const mapStateToProps = (state) => {
  return {
    dashboardData: state.dashboardDataFetchDataSuccess, 
    hasErrored: state.dashboardDataHasErrored, 
    isLoading: state.dashboardDataIsLoading,
    settings: state.settingsChanged
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    fetchData: (url) => dispatch(dashboardDataFetchData(url)),
    fetchSettings: (url) => dispatch(settingsFetchData(url)),
    saveSettings: (url, settings) => dispatch(postSettings(url, settings))
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(withStyles(styles)(Dashboard));