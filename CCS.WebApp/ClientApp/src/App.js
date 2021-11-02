import './App.css';
import { Dashboard } from './Dashboard';
import { Measures } from './Measures';
import { BrowserRouter, Route, Switch } from 'react-router-dom'
import Layout from './Layout';


export default () => (
    <BrowserRouter>
        <Switch>
            <Layout>
                <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500" />
                <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons" />    
                <Route exact path='/' component={Dashboard} />
                <Route path='/dashboard' component={Dashboard} />
                <Route path='/measures' component={Measures} />
            </Layout>
        </Switch>
    </BrowserRouter>
);

