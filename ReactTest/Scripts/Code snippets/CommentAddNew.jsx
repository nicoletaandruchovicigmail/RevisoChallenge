var Comment = React.createClass({
    getInitialState: function () {
        return { editing: false }
    },

    edit: function () {
        this.setState({ editing: true });
    },

    remove: function () {
        this.props.deleteFromBoard(this.props.index)
    },

    save: function () {
        var val = this.refs.newText.value;
        this.props.updateCommentText(val, this.props.index)
        this.setState({ editing: false });
    },

    renderNormal: function () {
        return (
            <div className="commentContainer">
                <div className="comment"> {this.props.children} </div>
                <button onClick={this.edit} className="button-primary"> Edit </button>
                <button onClick={this.remove} className="button-danger"> Remove </button>
            </div>
        );
    },

    renderForm: function () {
        return (
            <div className="commentContainer">
                <textarea ref="newText" defaultValue={this.props.children}></textarea>
                <button onClick={this.save} className="button-success"> Save </button>
            </div>
        );
    },

    render: function () {
        if (this.state.editing) {
            return this.renderForm();
        } else {
            return this.renderNormal();
        }

    }

});

var Board = React.createClass({
    getInitialState: function () {
        return {
            comments: [
                'I like bacon',
                'Want to get icecream?',
                'Ok, we have enough comments now'
            ]
        }
    },

    addComment: function (defaultText) {
        var arr = this.state.comments;
        arr.push(defaultText);
        this.setState({ comments: arr })
    },

    removeComment: function (i) {
        console.log('Removing comment: ' + i);
        var arr = this.state.comments;
        arr.splice(i, 1);
        this.setState({ comments: arr })
    },

    updateComment: function (newText, i) {
        console.log('Updating comment: ' + i);
        var arr = this.state.comments;
        arr[i] = newText;
        this.setState({ comments: arr })
    },


    eachComment: function (text, i) {
        return (
            <Comment key={i} index={i} updateCommentText={this.updateComment} deleteFromBoard={this.removeComment}>
                {text}
            </Comment>
        );
    },

    render: function () {
        return (
            <div>
                <button onClick={this.addComment.bind(null, "New Comment")} className="button-info create">Add new</button>
                <div className="Board">
                    {this.state.comments.map(this.eachComment)}
                </div>
            </div>
        );
    }

});

ReactDOM.render(<Board />, document.getElementById('container'));