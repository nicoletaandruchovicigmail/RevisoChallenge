    var Bacon = React.createClass(
    {
    render: function() {
    return (
    <div>
        <h3>This is a simple component</h3>
        <p> This is some bacon</p>
    </div>
    );
    }
    });

    ReactDOM.render(
    <div>
        <Bacon />
        <Bacon />
        <Bacon />
    </div>, document.getElementById('content'));
