import { BrowserRouter as Router, Route } from "react-router-dom";
import "antd/dist/antd.css";
import { Layout } from 'antd';

import List from './Toys/List';
import Edit from './Toys/Edit';

const { Header, Content } = Layout;

function App() {
  return (

      <Layout>
          <Header></Header>
          <Content style={{ padding: '50px' }}>
              <Router>
                  <Route exact path='/' component={List} />
                  <Route path='/add' component={Edit} />
                  <Route path='/edit/:id' component={Edit} />
              </Router>
          </Content>
      </Layout>
  );
}

export default App;
